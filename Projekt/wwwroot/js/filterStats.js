//let CurrentSurvey = null;

document.addEventListener("DOMContentLoaded", async function () {

    const filterDiv = document.getElementById("filter-div");
    
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

            const ageInput = document.createElement("input");
            ageInput.id = "age";
            ageInput.placeholder = "wprowadź wiek";

            filterDiv.appendChild(ageInput);
            filterDiv.appendChild(document.createElement(`br`));

            const button = document.createElement("button");
            button.type = "button";
            button.textContent = "Przejdź do ankiety";

            button.addEventListener("click", () => {
                window.location.href = `/SurveyResults/SurveyStats/`;
            });

            filterDiv.appendChild(button);

        })
})
