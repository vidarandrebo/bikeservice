//the get* functions defined here are used in serveral different components and are therefore not just a mehthod within the component.
async function getUsername():Promise<string> {
    let response = await fetch('/username', {
        method: 'GET',
        headers: {
            'Content-Type': 'application/json'
        }
    });
    let result = await response.json();
    let username = result["username"];
    return username;
}

async function postUser() {
    let response = await fetch('/User', {
        method: 'POST',
        headers: {
            'Content-Type': 'application/json'
        },
        body: JSON.stringify( {
                "id": 4,
                "userName": "vidar",
                "password": "123"
        }),

    });
    let result = await response;
}



function pathString(path:string) {
    let pathList = ["global", "user", "login", "register", "settings"]
    for (let i = 0; i < pathList.length; i++) {
        if (path.includes(pathList[i])) {
            return pathList[i];
        }
    }
    return "following"

}

