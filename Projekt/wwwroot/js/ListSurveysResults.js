document.addEventListener("DOMContentLoaded", async function () {

    const surveyListDiv = document.getElementById("survey-list");

    fetch('/SurveyResults/ListSurveysCompleted')
        .then(response => {
            if (!response.ok) throw new Error("Network Error");
            return response.json();
        })
        .then(surveys => {
            surveyListDiv.innerHTML = "";

            //surveys = null;

            if (!surveys || surveys.length === 0) {

                const paragraph = document.createElement("p");
                paragraph.textContent = "Brak statystyk do wyświetlenia."
                surveyListDiv.appendChild(paragraph);
                return;

            }

            surveys.forEach((survey) => {

                //isCompleted(survey.id).then(completed => {

                    //console.log(survey);

                    //if (!isCompleted(survey.id)) {
                    //    return;
                    //}

                    //if (!completed) return;

                    const button = document.createElement("button");
                    button.type = "button";
                    button.textContent = survey.title;
                    button.classList.add("btn", "btn-primary", "m-1");

                    button.addEventListener("click", function () {
                        saveSurveyToSession(survey.id);
                        //    window.location.href = `/SurveyResults/SurveyStats?surveyId=${survey.Id}`;
                        window.location.href = `/SurveyResults/SurveyStatsFilter`;

                    });

                    surveyListDiv.appendChild(button);
                    const br = document.createElement("br");
                    surveyListDiv.appendChild(br);
                //});
            })
        })
        //.catch(error => console.error("Error fetching surveys:", error));

                
});

function saveSurveyToSession(surveyId) {
    fetch('/Survey/SaveChoosenSurvey', {
        method: 'POST',
        headers: { 'Content-Type': 'application/json' },
        body: JSON.stringify(surveyId)
    })
        .then(res => {
            if (res.ok) {
                console.log("SurveyId zapisane w sesji:", surveyId);
            }
        })
        .catch(err => console.error("Error saving survey:", err));
}



//async function isCompleted(surveyId) {

//    try {
//        const response = await fetch(`/SurveyResults/IsCompleted?surveyId=${surveyId}`);
//        const result = await response.json();
//        console.log(result);
//        return result;
//    } catch (err) {
//        console.error(err);
//        return;
//    }
    

//}