


function openUp() {
    const down = document.querySelector(".down");
    down.style.display = "block";
}
const localUser = sessionStorage.getItem('user');

const currentId = JSON.parse(localUser).id;
const currentName = JSON.parse(localUser).firstName;
alert(currentName);
const div = document.querySelector(".hello");
div.innerHTML = `שלום ${currentName}`;
async function putResponse() {
    //try {
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
            Id:currentId,
            UserName: email.value,
            Password: pass.value,
            FirstName: f_name.value,
            LastName: l_name.value
        };
        const response = await fetch(`https://localhost:44341/api/Users/${currentId}`, {
            method: 'PUT',
            headers: {
                'Content-Type': 'application/json',
            },
            body: JSON.stringify(n_user)
        });
        //if (!response.ok) {
        //    throw new Error;
        //}
        const dataPost = await response;
        sessionStorage.setItem('user', JSON.stringify(n_user));
        alert("הפרטים עודכנו בהצלחה");
        return;
    //}
    //catch (e) {
    //    alert(e);
    //}
}