//this component contains the array of posts and creates the components that display the posts, the publish component, and the sorting-select
const MainSite = {
    props: ["user", "site"],
    template: `
            <div class="post" v-if="!user">
                <p>Welcome to the page! To like posts and follow other users, click the register button in the top right corner. Already have an account? Log in instead! Remember that you can view all of the post on the site by clicking on the globe, even if you are not logged in.</p>
            </div>
            <div class="post" v-if="user">
                <p>Welcome to the page! It seems you are able to log in</p>
            </div>
            <bike v-bind:user="user"></bike>
    `,

}
