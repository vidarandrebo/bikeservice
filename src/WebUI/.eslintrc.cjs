module.exports = {
    root: true,
    env: { browser: true, es2020: true },
    extends: ["eslint:recommended", "plugin:@typescript-eslint/recommended", "plugin:vue/vue3-recommended", "prettier"],
    rules: {
        // override/add rules settings here, such as:
        // 'vue/no-unused-vars': 'error'
    },
    parser: "vue-eslint-parser",
    parserOptions: {
        parser: "@typescript-eslint/parser"
    }
};
