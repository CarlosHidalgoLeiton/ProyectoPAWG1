$(document).ready(() => {
    $.ajax({
        url: '/Dashboard/LoadData',
        method: 'GET',
        contentType: 'application/json', 
        dataType: 'json', 
        success: function (response) {
            const userId = response.userId;
            const components = response.components;

            const hidden = components.filter(
                x => x.statuses.some(c => (c.userId == userId) && (c.type == "Hide"))
            );

            const favorites = components.filter(
                x => x.statuses.some(c => (c.userId == userId) && (c.type == "Favorite"))
            );

            const data = components.filter(x =>
                !x.statuses.some(c => c.userId == userId)
            );


            let weathers = [];
            let exchanges = [];
            let news = [];

            data.forEach(
                component => {
                    if (component.typeComponent == "Weather") {
                        weathers.push(component);
                    } else if (component.typeComponent == "Exchange Rate") {
                        exchanges.push(component);
                    } else if (component.typeComponent == "News") {
                        news.push(component);
                    }
                }
            );
            if (hidden.length > 0) {
                let divFavorite = document.getElementById("title_hidden");
                let a = document.createElement("a");
                const title = "Show Hidden";
                a.innerHTML = title + ` <i class="fa-solid fa-arrow-down"></i>`;

                a.classList.add("btn");
                a.classList.add("btn-secondary");
                a.setAttribute("data-bs-toggle", "collapse");
                a.setAttribute("href", "#collapseExample");
                a.setAttribute("role", "button");
                a.setAttribute("aria-expanded", "false");
                a.setAttribute("aria-controls", "collapseExample");
                divFavorite.appendChild(a);

                loadHidden(hidden)
            }

            if (favorites.length > 0) {
                let divFavorite = document.getElementById("title_favorite");
                let h3 = document.createElement("h3");
                const title = "My Favorites";

                h3.innerHTML = title;
                divFavorite.appendChild(h3);

                loadFavorite(favorites)
            }

            if (weathers.length > 0) {
                let divWeather = document.getElementById("title_weather");
                let h3 = document.createElement("h3");
                const title = "Weather";

                h3.innerHTML = title;
                divWeather.appendChild(h3);

                loadWeather(weathers, "weather");
            }

            if (exchanges.length > 0) {
                let divExchange = document.getElementById("title_exchange");
                let h3 = document.createElement("h3");
                const title = "Exchange Rate";

                h3.innerHTML = title;
                divExchange.appendChild(h3);

                loadExchange(exchanges, "exchange");
            }

            if (news.length > 0) {
                let divNews = document.getElementById("title_news");
                let h3 = document.createElement("h3");
                const title = "News";

                h3.innerHTML = title;
                divNews.appendChild(h3);

                loadNotice(news, "news");
            }
            
        },
        error: function (jqXHR, textStatus, errorThrown) {
            console.error('Error en la solicitud:', textStatus, errorThrown);
        }
    });

    timeRefresh();
});

const loadHidden = data => {
    let weathers = [];
    let exchanges = [];
    let news = [];

    data.forEach(
        component => {
            if (component.typeComponent == "Weather") {
                weathers.push(component);
            } else if (component.typeComponent == "Exchange Rate") {
                exchanges.push(component);
            } else if (component.typeComponent == "News") {
                news.push(component);
            }
        }
    );

    loadWeather(weathers, "hidden", false, true);
    loadExchange(exchanges, "hidden", false, true);
    loadNotice(news, "hidden", false, true);

}

const loadFavorite = data => {
    let weathers = [];
    let exchanges = [];
    let news = [];

    data.forEach(
        component => {
            if (component.typeComponent == "Weather") {
                weathers.push(component);
            } else if (component.typeComponent == "Exchange Rate") {
                exchanges.push(component);
            } else if (component.typeComponent == "News") {
                news.push(component);
            }
        }
    );

    loadWeather(weathers, "favorites", true);
    loadExchange(exchanges, "favorites", true);
    loadNotice(news, "favorites", true);

}

const loadWeather = (data, id, favorite = false, hidden = false) => {
    data.forEach(widget => {
        let html = document.createElement("div");
        html.className = `col-md-${widget.size} mb-4`;

        const data = JSON.parse(widget.data);

        let buttons;
        if (favorite) {
            buttons = `
                <div class="col-12">
                    ${buttonFavorite(widget.idComponent)}
                </div>
            `;
        } else if (hidden) {
            buttons = `
                <div class="col-12">
                    ${buttonHide(widget.idComponent)}
                </div>
            `;
        } else {
            buttons = `<div class="col-6">
                            ${buttonNoFavorite(widget.idComponent)}
                        </div>
                        <div class="col-6">
                            ${buttonShow(widget.idComponent)}
                        </div>`;
        }

        html.innerHTML = `
                <div class="card" style="background-color: ${widget.color};">
                    <div class="card-body p-3">
                        <div class="row mb-4">
                            <div class="col-7">
                                <div class="numbers">
                                    <p class="text-sm mb-0 text-capitalize font-weight-bold text-dark">${data.city.name}</p>
                                    <h5 class="font-weight-bolder mb-0 text-dark">
                                        ${data.list[0].main.temp}°
                                        <span class=" text-sm font-weight-bolder text-dark">${data.city.country}</span>
                                    </h5>
                                </div>
                            </div>
                            <div class="col-5 text-end d-flex justify-content-end">
                                <div class="col-md-6 icon-shape shadow bg-gradient-white text-center border-radius-md">
                                    <img src="data:image/png;base64,${widget.simbol}" alt="Component Icon" class="img-fluid img-simbol"/>
                                </div>
                            </div>
                        </div>
                        <div class="row">
                            ${buttons}
                        </div>
                    </div>
                </div>
        `;

        let weatherElement = document.getElementById(id);

        weatherElement.appendChild(html);
    });
};

const loadExchange = (data, id, favorite = false, hidden = false) => {
    data.forEach(table => {
        let html = document.createElement("div");
        html.className = `col-md-${table.size} mb-4`;

        const data = JSON.parse(table.data);

        let buttons;
        if (favorite) {
            buttons = `
                <div class="col-4">
                    ${buttonFavorite(table.idComponent)}
                </div>
            `;
        } else if (hidden) {
            buttons = `
                <div class="col-4">
                    ${buttonHide(table.idComponent)}
                </div>
            `;
        } else {
            buttons = `<div class="col-lg-2 mb-3">
                            ${buttonNoFavorite(table.idComponent)}
                        </div>
                        <div class="col-lg-2 mb-3">
                            ${buttonShow(table.idComponent)}
                        </div>`;
        }
        
        html.innerHTML = `
                <div class="card" style="background-color: ${table.color}">
                    <div class="card-header pb-0" style="background-color: ${table.color}">
                        <div class="row">
                            <div class="col-lg-6 mb-3">
                                <h6 class="text-dark">${table.typeComponent}</h6>
                                <p class="text-sm mb-0 text-dark">
                                    <span class="font-weight-bold ms-1">Type:</span> ${data.base_code}
                                </p>
                            </div>
                            <div class="col-lg-2 mb-3">
                                 <div class="icon-shape shadow bg-gradient-white text-center border-radius-md">
                                        <img src="data:image/png;base64,${table.simbol}" alt="Component Icon" class="img-fluid"/>
                                </div>
                            </div>
                            ${buttons}
                        </div>
                    </div>
                        <div class="card-body px-0 pb-2">
                        <div class="table-responsive">
                            <table class="table align-items-center mb-0">
                                <thead>
                                    <tr>
                                        <th class="text-uppercase text-xxs font-weight-bolder text-dark">Country</th>
                                        <th class="text-uppercase text-xxs font-weight-bolder text-dark">Value</th>
                                    </tr>
                                </thead>
                                ${loadTableExchange(data)}
                            </table>
                        </div>
                    </div>
                </div>
        `;

        let weatherElement = document.getElementById(id);

        weatherElement.appendChild(html);
    });
};

const loadNotice = (data, id, favorite = false, hidden = false) => {
    data.forEach(card => {
        let html = document.createElement("div");
        html.className = `col-md-${card.size} mb-4`;

        const data = JSON.parse(card.data)

        let buttons;
        if (favorite) {
            buttons = `
                <div class="col-12">
                    ${buttonFavorite(card.idComponent)}
                </div>
            `;
        } else if (hidden) {
            buttons = `
                <div class="col-12">
                    ${buttonHide(card.idComponent)}
                </div>
            `;
        } else {
            buttons = `<div class="col-6">
                            ${buttonNoFavorite(card.idComponent)}
                        </div>
                        <div class="col-6">
                            ${buttonShow(card.idComponent)}
                        </div>`;
        }

        html.innerHTML = `
            <div class="card" style="background-color: ${card.color}">
                <div class="card-body p-3">
                    <div class="row mb-4">
                        <div class="col-lg-6">
                            <div class="d-flex flex-column h-100">
                                <p class="mb-1 pt-2 text-bold">Author: ${data.articles[0].author}</p>
                                <h5 class="font-weight-bolder text-dark"> ${data.articles[0].title} </h5>
                                <p class="mb-5 text-dark"> ${data.articles[0].description} </p>
                                <a class="text-body text-sm font-weight-bold mb-0 icon-move-right mt-auto" href=${data.articles[0].url}>
                                    Read More
                                    <i class="fas fa-arrow-right text-sm ms-1" aria-hidden="true"></i>
                                </a>
                            </div>
                        </div>
                        <div class="col-lg-5 ms-auto text-center mt-5 mt-lg-0">
                            <div class="position-relative d-flex align-items-center justify-content-center h-100">
                                <img class="w-100 position-relative z-index-2 pt-4" src="data:image/png;base64,${card.simbol}" alt="rocket">
                            </div>
                        </div>
                    </div>
                    <div class="row">
                        ${buttons}
                    </div>
                </div>
            </div>
        `;


        let weatherElement = document.getElementById(id);

        weatherElement.appendChild(html);
    });
}


const loadTableExchange = data => {
    let tbody = document.createElement("tbody");

    const currencies = Object.keys(data.conversion_rates).slice(0, 10);

    currencies.forEach(currency => {
        let row = document.createElement("tr");

        let country = document.createElement("td");
        let value = document.createElement("td");

        country.className = `text-dark`;
        value.className = `text-dark`;

        country.innerHTML = currency;  
        value.innerHTML = data.conversion_rates[currency];  

        row.appendChild(country);
        row.appendChild(value);

        tbody.appendChild(row);
    });

    return tbody.innerHTML;
    
}


const buttonFavorite = id => {
    return `<form action="/Dashboard/DeleteStatus" method="post">
                <input type="hidden" name="id" value="${id}" />
                <button type="submit" class="btn btn-noFavorite">
                    <i class="fa-solid fa-heart heart"></i>
                </button>
            </form>`;
};

const buttonNoFavorite = id => {
    return `<form action="/Dashboard/SaveStatus" method="post">
                <input type="hidden" name="id" value="${id}" />
                <input type="hidden" name="type" value="Favorite" />
                <button type="submit" class="btn btn-favorite">
                    <i class="fa-regular fa-heart heart"></i>
                </button>
            </form>`;
};

const buttonHide = id => {
    return `<form action="/Dashboard/DeleteStatus" method="post" >
                <input type="hidden" name="id" value="${id}" />
                <button type="submit" class="btn btn-hide">
                    <i class="fa-solid fa-eye"></i>
                </button>
            </form>`;
};

const buttonShow = id => {
    return `<form action="/Dashboard/SaveStatus" method="post">
                <input type="hidden" name="id" value="${id}" />
                <input type="hidden" name="type" value="Hide" />
                <button type="submit" class="btn btn-show">
                    <i class="fa-solid fa-eye-slash"></i>
                </button>
            </form>`;
}

const timeRefresh = () => {
    let time = document.getElementById("timeRefresh").innerHTML;
    time = time * 60 * 1000;
    setTimeout(() => {
        location.reload();
    }, time);
}
