async function getUsername(): Promise<string> {
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


function pathString(path: string) {
    let pathList: Array<string> = ["global", "user", "login", "register", "settings"]
    for (let i = 0; i < pathList.length; i++) {
        if (path.includes(pathList[i])) {
            return pathList[i];
        }
    }
    return "following"

}

