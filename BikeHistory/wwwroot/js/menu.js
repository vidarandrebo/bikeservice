const Menu = {
    props: ["user"],
    template: `
            <div class="menugroup_left menugroup">
                <h3>BikeHistory</h3>
            </div>
            <div class="menugroup_middle menugroup">
                <router-link to="/"><i class="fa fa-bicycle fa-2x"></i></router-link>
                <router-link to="/global"><i class="fa fa-wrench fa-2x"></i></router-link>
            </div>
            <div class="menugroup_right menugroup">
                <p v-if="user" id="username">{{ user }}</p>
                <router-link v-if="user" to="/settings"><i class="fa fa-cog fa-2x"></i></router-link>
                <a v-if="user" href="#" v-on:click="logout"><i class="fa fa-sign-out fa-2x"></i></a>
                <router-link v-if="!user" to="/login"><i class="fa fa-sign-in fa-2x"></i></router-link>
                <router-link v-if="!user" to="/register"><i class="fa fa-user-plus fa-2x"></i></router-link>
            </div>
            `,
    methods: {
        logout: async function() {
            let response = await fetch('/logout', {
                method: 'GET',
                headers: {
                    'Content-Type': 'application/json'
                }
            });
            let result = await response.json();
            if (result["logout"] == "success") {
                this.$root.user = await getUsername();
                router.push("/login");
            }
        }
    }
}
