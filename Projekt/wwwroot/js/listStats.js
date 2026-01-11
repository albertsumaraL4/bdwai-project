document.addEventListener("DOMContentLoaded", async function () {
    const questionListDiv = document.getElementById("question-list");

    const surveyId = await getSurveyId();
    if (!surveyId) return;
    CurrentSurvey = surveyId;
    const stats = await getStats(CurrentSurvey);

        fetch(`/SurveyResults/GetResults?surveyId=${surveyId}`)
            .then(response => {
                if (!response.ok) throw new Error("Network Error");
                return response.json();
            })
            .then(questions => {

                //console.log(questions);

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

                    listAnswers(question.id, questionDiv, stats[question.id]);

                });

            })
            .catch(error => console.error("Error fetching questions:", error));
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

function listAnswers(questionId, containerDiv, answerStats) {
    fetch(`/Survey/ListAnswers?questionId=${questionId}`)
        .then(response => response.json())
        .then(answers => {

            let total = 0;
            for (const answerId in answerStats) {
                total += answerStats[answerId];
            }

            answers.forEach(answer => {

                const paragraph = document.createElement("p");
                paragraph.textContent = answer.content;

                const strong = document.createElement("strong")

                if (answer.id in answerStats) {

                    console.log("jest");
                   strong.textContent = `    ${Math.round(answerStats[answer.id] / total * 100)}%`


                }

                else {
                    strong.textContent = `    0%`
                }

                paragraph.appendChild(strong);
                containerDiv.appendChild(paragraph);


            });
        })
        .catch(error => console.error("Error fetching answers:", error));
}

async function getStats(surveyId) {
    try {
        const response = await fetch(`/SurveyResults/GetStats?surveyId=${surveyId}`);
        const stats = await response.json();
        //console.log("stats", stats)
        return stats;
    } catch (err) {
        console.error(err);
        return false;
    }
}