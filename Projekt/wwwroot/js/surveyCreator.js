const survey = {
    title: "",
    questions: []
};

const question = {

    content: "",
    answers: []

}




document.addEventListener("DOMContentLoaded", async function () {
    //const surveyTitleDiv = document.getElementById("survey-title");
    const titles = await getTitles();
    const errorDiv = document.createElement("div");
    errorDiv.id = "error";


    document.getElementById("setTitleBtn").addEventListener("click", async () => {
        clearError();
        const titleInput = document.getElementById("surveyTitle");
        const title = titleInput.value.trim();
        //titleInput.textContent = "Dalej";


        if (checkTitle(titles, title)){
            showError("The title is already taken.");
            return;
        }

        if (!title) {
            showError("Title cannot be empty!");

            return;
        }

        if (title.length < 3) {
            showError("Title must have at least 3 characters!");


            return;
        }

        survey.title = titleInput.value;
        titleInput.value = "";
        console.log(survey);

        //window.location.href = "/Survey/CreateSurveyQuestion";

        document.getElementById("step-title").style.display = "none";
        document.getElementById("step-question").style.display = "block";

    });


    document.getElementById("setQuestionContentBtn").addEventListener("click", () => {
        clearError();
        const questionContentInput = document.getElementById("surveyQuestionContent");
        const questionContent = questionContentInput.value.trim();
        //questionContentInput.value = "";

        //questionContentInput.textContent = "Dalej";

        console.log(survey);
        console.log(question);

        if (!questionContent) {
            showError("Content of question cannot be empty!");
            return;
        }

        if (questionContent.length < 8) {
            showError("Title must have at least 8 characters!");
            return;
        }

        question.content = questionContentInput.value;
        questionContentInput.value = "";


        document.getElementById("step-question").style.display = "none";
        document.getElementById("step-answer").style.display = "block";

        //console.log(survey);

        //window.location.href = "/Survey/CreateSurveyAnswer";

    });

    document.getElementById("setAnswerContentBtn").addEventListener("click", () => {
        clearError();
        const answerContentInput = document.getElementById("surveyAnswerContent");
        const answerContent = answerContentInput.value.trim();
        //answerContentInput.value = "";

        //answerContentInput.textContent = "Dodaj odpowiedź";


        if (!answerContent) {
            showError("Content of answer cannot be empty!");

            return;
        }

        //if (Answer.length > 10) {
        //    alert("Content of question cannot exceed 10 characters!");
        //    return;
        //}

        question.answers.push(answerContentInput.value);

        answerContentInput.value = "";

        //window.location.href = "/Survey/CreateSurveyAnswer";

    });


    document.getElementById("setQuestion").addEventListener("click", () => {
        clearError();
        //questionInput.textContent = "Dodaj następne pytanie";


        if (question.answers.length < 2) {
            showError("Question must have at least 2 answers!");

            //window.location.href = "/Survey/CreateSurveyAnswer";
            return;
        }

        survey.questions.push({ ...question });

        question.content = "";
        question.answers = [];

        //document.getElementById("step-answer").style.display = "none";
        //document.getElementById("step-question").style.display = "block";
        //window.location.href = "/Survey/CreateSurveyQuestion";
    });

    document.getElementById("setSurvey").addEventListener("click", () => {
        clearError();
        //questionInput.textContent = "Dodaj następne pytanie";


        if (survey.questions.length < 1) {
            showError("Survey must have at least 1 question!");


            //window.location.href = "/Survey/CreateSurveyAnswer";
            return;
        }

        submitSurvey();

        survey.title = "";
        survey.questions = [];

        document.getElementById("step-answer").style.display = "none";
        document.getElementById("step-title").style.display = "block";

        //window.location.href = "/Survey/CreateSurveyQuestion";
    });

    document.getElementById("backToQuestion").addEventListener("click", () => {
        clearError();
        document.getElementById("step-answer").style.display = "none";
        document.getElementById("step-question").style.display = "block";
        question.content = "";
    });

    document.getElementById("backToTitle").addEventListener("click", () => {
        clearError();
        document.getElementById("step-question").style.display = "none";
        document.getElementById("step-title").style.display = "block";
        survey.title = "";
    });



});

function submitSurvey() {

    fetch("/Survey/Results", {
        method: "POST",
        headers: {
            "Content-Type": "application/json"
        },
        body: JSON.stringify(survey)
    })
        .then(res => res.json())
        .then(data => {
            if (data.success) {
                console.log("Survey saved successfully! ID: " + data.surveyId);
                //window.location.href = "/Survey/SurveyCreator";
            } else {
                showError("Error saving survey");
            }
        })
        .catch(err => {
            console.error(err);
            showError("Something went wrong!");
        });
}

function showError(message) {
    const errorDiv = document.getElementById("error");
    errorDiv.style.color = "red";
    errorDiv.textContent = message;
}

function clearError() {
    const errorDiv = document.getElementById("error");
    errorDiv.textContent = "";
}

async function getTitles() {
    try {
        const response = await fetch("/Survey/ListSurveysTitles");
        const titles = await response.json();
        console.log(titles)
        return titles;
    } catch (err) {
        console.error(err);
        return false;
    }
}

function checkTitle(titles, title) {

    return titles.includes(title);

}