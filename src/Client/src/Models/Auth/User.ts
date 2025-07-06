import { ref, Ref } from "vue";
import { ObjectAssignable } from "../ObjectAssignable.ts";
import { getAuthApi } from "../Api.ts";

export type UserDependency = {
    user: Ref<User>;
    setUser(user: User): void;
};

export function DefaultUserDependency(): UserDependency {
    return {
        user: ref<User>(new User()),
        setUser: () => {}
    };
}

export class User extends ObjectAssignable {
    email: string;
    accessToken: string;
    refreshToken: string;

    writeToLocalStorage() {
        localStorage.setItem("user", JSON.stringify(this));
    }

    removeFromLocalStorage() {
        localStorage.removeItem("user");
    }

    constructor() {
        super();
        this.email = "";
        this.accessToken = "";
        this.refreshToken = "";
    }

    async refresh(): Promise<User | null> {
        const client = getAuthApi();

        try {
            const response = await client.apiAuthRefreshPost({ refreshRequest: { refreshToken: this.refreshToken } });
            this.refreshToken = response.refreshToken;
            this.accessToken = response.accessToken;
            this.writeToLocalStorage();
            return this;
        } catch {
            this.removeFromLocalStorage();
        }
        return null;
    }
}

export function loadUserFromLocalStorage(): User | null {
    const data = localStorage.getItem("user");
    if (data != null) {
        const parsedData = JSON.parse(data);
        const user = new User();
        user.assignFromObject(parsedData);
        return user;
    }
    return null;
}

export function loadBearerTokenFromLocalStorage(): string | null {
    const user = loadUserFromLocalStorage();
    if (user) {
        return user.accessToken;
    }
    return null;
}
