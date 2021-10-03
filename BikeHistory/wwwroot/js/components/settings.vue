const Settings = {
    props: ["user"],
    template: `
            <div class="settings" v-if="user">
                <form id="changePasswd" method="POST" v-on:submit="changePasswd">
                    <p v-if="error">{{ error }}</p>
                    <label for="oldpasswd">Old Password</label>
                    <input type="password" name="oldpasswd" id="oldpasswd" v-model="oldPasswd" required>
                    <label for="newpasswd">New Password</label>
                    <input type="password" name="newpasswd" id="newpasswd" v-model="newPasswd" required>
                    <label for="renewpasswd">Repeat New Password</label>
                    <input type="password" name="renewpasswd" id="renewpasswd" v-model="renewPasswd" required>
                    <input type="submit" value="Change Password">
                </form>
            </div>
            <div class="post" v-if="!user">
                <p>Welcome to the settings page! Since you are not logged in, you have no settings to change!</p>
            </div>
    `,
    data: function() {
        return {
            oldPasswd: null,
            newPasswd: null,
            renewPasswd: null,
            error: null
        }
    },
    methods: {
        changePasswd: async function() {
            event.preventDefault();
            this.error = null;
            if (this.newPasswd !== this.renewPasswd) {
                this.error = "Passwords do not match";
                return;
            }
            if (this.newPasswd.length < 8) {
                this.error = "Password should be 8 characters or longer!";
                return;
            }
            let passwords = {
                "oldPasswd": this.oldPasswd,
                "newPasswd": this.newPasswd
            };
            let response = await fetch('/passwd', {
                method: 'PUT',
                headers: {
                    'Content-Type': 'application/json'
                },
                body: JSON.stringify(passwords)
            });
            let result = await response.json();
            if (result['change'] == "success") {
                this.error = "Password changed!"
                this.oldPasswd = this.renewPasswd = this.newPasswd = null;
            } else {
                this.error = result["error"];
            }
        },
    }
}
