
const prog = document.querySelector(".progress");


async function getResponse() {
    try {
        const response = await fetch("https://localhost:44341/api/Users");
        if (!response.ok) {
            throw new Error(`HTTP error! status:${response.status}`);
        }
        const data = await response.json();
    }
    catch (e) {
        alert(e);
    }
}






async function postResponse() {


    try {
        const email = document.querySelector("#userName");
        const pass = document.querySelector("#password");
        const f_name = document.querySelector("#firstName");
        const l_name = document.querySelector("#lastName");




        if (email.value == "" || pass.value == "" ||
            f_name.value == "" || l_name.value == "") {
            alert("הכנס את כל הנתונים");
            return;
        }



     

        const n_user = {
            UserName: email.value,
            Password: pass.value,
            FirstName: f_name.value,
            LastName: l_name.value
        };

        const response = await fetch("https://localhost:44341/api/Users", {
            method: 'Post',
            headers: {
                'Content-Type': 'application/json',
            },
            body: JSON.stringify(n_user)
        });
        if (response.status == 400) {
            alert("הכנס סיסמה חזקה");
            return;
        }
        if (!response.ok) {
            throw new Error;
        }
        alert("המשתמש נרשם בהצלחה");
        const dataPost = await response.json();
    }
    catch (e) {
        alert(e);
    }
}


async function loginResponse() {

    try {
        const e_email = document.querySelector("#e_userName");
        const e_pass = document.querySelector("#e_password");

        if (e_email.value == "" || e_pass.value == "") {
            alert("הכנס את כל הנתונים");
            return;
        }

        const e_user = {
            UserName: e_email.value,
            Password: e_pass.value,
            FirstName: "",
            LastName: ""
        }


        const response = await fetch("https://localhost:44341/api/Users/Login", {
            method: 'Post',
            headers: {
                'Content-Type': 'application/json',
            },
            body: JSON.stringify(e_user)
        });


        if (response.status == 204) {
            alert("שם משתמש או סיסמה אינם תקינים");
            return;
        }
        if (!response.ok) {
            throw new Error;
        }
        const dataPost = await response.json();
        sessionStorage.setItem('user', JSON.stringify(dataPost));


        window.location.href = "NewFolder2\\page2.html";
    }
    catch (e) {
        alert(e);
    }

}
async function checkPassword() {
    const pass = document.querySelector("#password");
    const password = {
        ThePassword: pass.value,
        Level: 0,
    }
    try {
        const response = await fetch("https://localhost:44341/api/Password", {
            method: 'Post',
            headers: {
                'Content-Type': 'application/json',
            },
            body: JSON.stringify(password)
        });
        //if (!response.ok) {
        //    throw new Error(`HTTP error! status:${response.status}`);
        //}
        const data = await response.json();
        prog.value = data.level * 25;
        if (response.ok) {
            return data.level / 4;
        }
        else {
            return 0;
        }

        alert(data.level);
    }
    catch (e) {
        alert(e);
    }
}