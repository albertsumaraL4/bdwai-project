document.addEventListener("DOMContentLoaded", function () {

    const surveyListDiv = document.getElementById("survey-list");

    fetch('/Survey/ListSurveys')
        .then(response => {
            if (!response.ok) throw new Error("Network Error");
            return response.json();
        })
        .then(surveys => {
            surveyListDiv.innerHTML = "";

            surveys.forEach((survey) => {
                console.log(survey);
                const button = document.createElement("button");
                button.type = "button";
                button.textContent = survey.title;
                button.classList.add("btn", "btn-primary", "m-1");

                button.addEventListener("click", function () {
                    saveSurveyToSession(survey.id);
                    window.location.href = `/SurveyResults/SurveyStats?surveyId=${survey.Id}`;
                });

                surveyListDiv.appendChild(button);

                const br = document.createElement("br");
                surveyListDiv.appendChild(br);
            });
        })
        .catch(error => console.error("Error fetching surveys:", error));

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