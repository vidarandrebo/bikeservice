module.exports = {
    root: true,
    env: { browser: true, es2020: true },
    extends: ["eslint:recommended", "plugin:@typescript-eslint/recommended", "plugin:vue/vue3-recommended", "prettier"],
    rules: {
        // override/add rules settings here, such as:
        // 'vue/no-unused-vars': 'error'
        "vue/v-on-event-hyphenation": [
            0,
            {
                autofix: false,
                ignore: []
            }
        ],
        "vue/attribute-hyphenation": [
            0,
            {
                autofix: false,
                ignore: []
            }
        ]
    },
    parser: "vue-eslint-parser",
    parserOptions: {
        parser: "@typescript-eslint/parser"
    }
};
