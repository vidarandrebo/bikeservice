import { ref, Ref } from "vue";

export class User {
    username: string;

    constructor(username: string) {
        this.username = username;
    }
}

export type UserDependency = {
    user: Ref<User>;
    setUser(user: User): void;
};

export function DefaultUserDependency(): UserDependency {
    return {
        user: ref<User>(new User("")),
        setUser: () => {}
    };
}
