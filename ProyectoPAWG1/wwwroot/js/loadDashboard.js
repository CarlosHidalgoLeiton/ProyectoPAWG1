$(document).ready(() => {
    $.ajax({
        url: '/Dashboard/LoadData',
        method: 'GET',
        contentType: 'application/json', 
        dataType: 'json', 
        success: function (response) {

            const favorites = response.filter(x =>
                x.users.some(c => c.idUser == 1)
            );

            const data = response.filter(x =>
                !x.users.some(c => c.idUser == 1)
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
});

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

const loadWeather = (data, id, favorite = false) => {
    data.forEach(widget => {
        let html = document.createElement("div");
        html.className = `col-md-${widget.size} mb-4`;

        const data = JSON.parse(widget.data)

        const isFavorite = `<form action="/Dashboard/DeleteFavorite" method="post" style="width:100%; height:100%">
                                <input type="hidden" name="id" value="${widget.idComponent}" />
                                <button type="submit" class="btn btn-noFavorite">
                                    <i class="fa-regular fa-heart heart"></i>
                                </button>
                            </form>`;

        const noFavorite = `<form action="/Dashboard/SaveFavorite" method="post" style="width:100%; height:100%">
                                <input type="hidden" name="id" value="${widget.idComponent}" />
                                <button type="submit" class="btn btn-favorite">
                                    <i class="fa-regular fa-heart heart"></i>
                                </button>
                            </form>`;

        html.innerHTML = `
                <div class="card" style="background-color: ${widget.color};">
                    <div class="card-body p-3">
                        <div class="row mb-4">
                            <div class="col-8">
                                <div class="numbers">
                                    <p class="text-sm mb-0 text-capitalize font-weight-bold text-dark">${data.city.name}</p>
                                    <h5 class="font-weight-bolder mb-0 text-dark">
                                        ${data.list[0].main.temp}°
                                        <span class=" text-sm font-weight-bolder text-dark">${data.city.country}</span>
                                    </h5>
                                </div>
                            </div>
                            <div class="col-4 text-end d-flex justify-content-end">
                                <div class="icon-shape shadow bg-gradient-white text-center border-radius-md">
                                        <img src="data:image/png;base64,${widget.simbol}" alt="Component Icon" class="img-fluid"/>
                                </div>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-12">
                                ${ favorite ? isFavorite : noFavorite }
                            </div>
                        </div>
                    </div>
                </div>
        `;

        let weatherElement = document.getElementById(id);

        weatherElement.appendChild(html);
    });
};

const loadExchange = (data, id, favorite = false) => {
    data.forEach(table => {
        let html = document.createElement("div");
        html.className = `col-md-${table.size} mb-4`;

        const data = JSON.parse(table.data)

        const isFavorite = `<form action="/Dashboard/DeleteFavorite" method="post" style="width:100%; height:100%">
                                <input type="hidden" name="id" value="${table.idComponent}" />
                                <button type="submit" class="btn btn-noFavorite">
                                    <i class="fa-regular fa-heart heart"></i>
                                </button>
                            </form>`;

        const noFavorite = `<form action="/Dashboard/SaveFavorite" method="post" style="width:100%; height:100%">
                                <input type="hidden" name="id" value="${table.idComponent}" />
                                <button type="submit" class="btn btn-favorite">
                                    <i class="fa-regular fa-heart heart"></i>
                                </button>
                            </form>`;

        html.innerHTML = `
                <div class="card" style="background-color: ${table.color}">
                    <div class="card-header pb-0" style="background-color: ${table.color}">
                        <div class="row">
                            <div class="col-lg-8">
                                <h6 class="text-dark">${table.typeComponent}</h6>
                                <p class="text-sm mb-0 text-dark">
                                    <span class="font-weight-bold ms-1">Type:</span> ${data.base_code}
                                </p>
                            </div>
                            <div class="col-lg-2">
                                 <div class="icon-shape shadow bg-gradient-white text-center border-radius-md">
                                        <img src="data:image/png;base64,${table.simbol}" alt="Component Icon" class="img-fluid"/>
                                </div>
                            </div>
                            <div class="col-lg-2">
                                ${ favorite ? isFavorite : noFavorite }
                            </div>
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

const loadNotice = (data, id, favorite = false) => {
    data.forEach(card => {
        let html = document.createElement("div");
        html.className = `col-md-${card.size} mb-4`;

        const data = JSON.parse(card.data)

        const isFavorite = `<form action="/Dashboard/DeleteFavorite" method="post" style="width:100%; height:100%">
                                <input type="hidden" name="id" value="${card.idComponent}" />
                                <button type="submit" class="btn btn-noFavorite">
                                    <i class="fa-regular fa-heart heart"></i>
                                </button>
                            </form>`;

        const noFavorite = `<form action="/Dashboard/SaveFavorite" method="post" style="width:100%; height:100%">
                                <input type="hidden" name="id" value="${card.idComponent}" />
                                <button type="submit" class="btn btn-favorite">
                                    <i class="fa-regular fa-heart heart"></i>
                                </button>
                            </form>`;

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
                        <div class="col-12">
                            ${ favorite ? isFavorite : noFavorite }
                        </div>
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