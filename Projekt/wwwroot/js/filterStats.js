//let CurrentSurvey = null;

document.addEventListener("DOMContentLoaded", async function () {

    const filterDiv = document.getElementById("filter-div");
    const errorDiv = document.createElement("div");
    errorDiv.style.color = "red";
    errorDiv.id = "errorDiv";
    
    fetch('/SurveyResults/GetCities')
        .then(response => {
            if (!response.ok) throw new Error("Network Error");
            return response.json();
        })
        .then(cities => {

            

            if (!cities || cities.length === 0) {

                const paragraph = document.createElement("p");
                paragraph.style.color = "red";
                paragraph.textContent = "Błąd. Brak miast."
                filterDiv.appendChild(paragraph);
                return;

            }

            const strong = document.createElement("strong");
            strong.textContent = "Filtruj po mieście: ";
            filterDiv.appendChild(strong);
            filterDiv.appendChild(document.createElement(`br`));

            const select = document.createElement("select");
            select.id = "citySelect";
            const option = document.createElement("option");
            option.value = 0;
            option.textContent = "brak";
            select.appendChild(option);

            cities.forEach((city) => {

                console.log(city);
                const option = document.createElement("option");
                option.value = city.id;
                option.textContent = city.name;

                

                select.appendChild(option);

            });

            filterDiv.appendChild(select);

            filterDiv.appendChild(document.createElement(`br`));
            const strongAge = document.createElement("strong");
            strongAge.textContent = "Filtruj po wieku: ";
            filterDiv.appendChild(strongAge);
            filterDiv.appendChild(document.createElement(`br`));

            const ageFromInput = document.createElement("input");
            ageFromInput.id = "ageFrom";
            ageFromInput.value = 18;
            ageFromInput.placeholder = "od";
            filterDiv.appendChild(ageFromInput);


            const strongChar = document.createElement("strong");
            strongChar.textContent = "-";
            filterDiv.appendChild(strongChar);

            const ageToInput = document.createElement("input");
            ageToInput.id = "ageTo";
            ageToInput.value = 99;
            ageToInput.placeholder = "od";
            filterDiv.appendChild(ageToInput);

            filterDiv.appendChild(document.createElement(`br`));

            const button = document.createElement("button");
            button.type = "button";
            button.textContent = "Przejdź do ankiety";

            button.addEventListener("click", () => {
                const ageFrom = Number(ageFromInput.value);
                const ageTo = Number(ageToInput.value);
                const cityId = Number(select.value);


                if (Number.isNaN(ageFrom) || ageFrom < 18 || ageFrom > 99) {
                    errorDiv.textContent = "Niepoprawna wartość dla wieku minimalnego.";
                    return;
                }

                if (Number.isNaN(ageTo) || ageTo < 18 || ageTo > 99) {
                    errorDiv.textContent = "Niepoprawna wartość dla wieku maksymalnego.";
                    return;
                }

                if (ageFrom > ageTo) {
                    errorDiv.textContent = "Wiek minimalny nie może być większy niż maksymalny.";
                    return;
                }


                saveFiltersToSession(cityId, ageFrom, ageTo);

                window.location.href = "/SurveyResults/SurveyStats/";
            });

            filterDiv.appendChild(errorDiv);
            filterDiv.appendChild(button);

        })
})


function saveFiltersToSession(cityId, ageFrom, ageTo) {
    fetch('/SurveyResults/SaveChoosenFilters', {
        method: 'POST',
        headers: { 'Content-Type': 'application/json' },
        body: JSON.stringify({

            cityId: cityId,
            ageFrom: ageFrom,
            ageTo: ageTo

        } )
    })
        .then(res => {
            if (res.ok) {
                console.log("Filtry zapisane w sesji:", { cityId, ageFrom, ageTo });
            }
        })
        .catch(err => console.error("Error saving filters:", err));
}