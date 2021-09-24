const routes = [
    { path: '/', component: MainSite, props: {site: "following"}},
    { path: '/global', component: MainSite, props: {site: "global"}},
    { path: '/user/:id', component: MainSite, props: {site: "user"}},
    { path: '/settings', component: Settings},
    { path: '/register', component: Register},
    { path: '/login', component: Login}
];
const router = VueRouter.createRouter({
    history: VueRouter.createWebHashHistory(),
    routes: routes
});

//the get* functions defined here are used in serveral different components and are therefore not just a mehthod within the component.
async function getUsername() {
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



function pathString(path) {
    let pathList = ["global", "user", "login", "register", "settings"]
    for (let i = 0; i < pathList.length; i++) {
        if (path.includes(pathList[i])) {
            return pathList[i];
        }
    }
    return "following"

}

let app = Vue.createApp({
    data: function(){
        return {
            user: null,
        }
    },
    //needed for fetching username when page is refreshed
    created: async function() {
        //this.user = await getUsername();
        await postUser();
    }
});
app.use(router);
app.component('bike', Bike);
app.component('mainsite', MainSite);
app.component('menubar', Menu);
app.component('settings', Settings);
app.component('register', Register);
app.component('login', Login);
app.mount("#app");
