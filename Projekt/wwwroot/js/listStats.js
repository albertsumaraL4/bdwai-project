document.addEventListener("DOMContentLoaded", function () {
    const questionListDiv = document.getElementById("question-list");

    getSurveyId().then(surveyId => {
        if (!surveyId) return;
        CurrentSurvey = surveyId;

        fetch(`/SurveyResults/GetResults?surveyId=${surveyId}`)
            .then(response => {
                if (!response.ok) throw new Error("Network Error");
                return response.json();
            })
            .then(questions => {

                console.log(questions);

            });

        fetch(`/Survey/ListQuestions?surveyId=${surveyId}`)
            .then(response => {
                if (!response.ok) throw new Error("Network Error");
                return response.json();
            })
            .then(questions => {
                questionListDiv.innerHTML = "";

                questions.forEach(question => {

                    const questionDiv = document.createElement("div");
                    const questionTitle = document.createElement("h4");
                    questionTitle.textContent = question.content;
                    questionDiv.appendChild(questionTitle);
                    questionListDiv.appendChild(questionDiv);


                    listAnswers(question.id, questionDiv);

                });

            })
            .catch(error => console.error("Error fetching questions:", error));
    });
});

function getSurveyId() {
    return fetch('/Survey/GetChoosenSurvey')
        .then(res => res.json())
        .then(data => {
            if (data.surveyId > 0) {
                return data.surveyId;
            } else {
                return null;
            }
        });
}

function listAnswers(questionId, containerDiv) {
    fetch(`/Survey/ListAnswers?questionId=${questionId}`)
        .then(response => response.json())
        .then(answers => {
            answers.forEach(answer => {

                const paragraph = document.createElement("p");
                paragraph.textContent = answer.content;

                containerDiv.appendChild(paragraph);


            });
        })
        .catch(error => console.error("Error fetching answers:", error));
}

