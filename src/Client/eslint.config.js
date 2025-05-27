import eslint from "@eslint/js";
import eslintConfigPrettier from "eslint-config-prettier";
import eslintPluginVue from "eslint-plugin-vue";
import globals from "globals";
import typescriptEslint from "typescript-eslint";

export default typescriptEslint.config(
    { ignores: ["*.d.ts", "**/coverage", "**/dist"] },
    {
        extends: [
            eslint.configs.recommended,
            ...typescriptEslint.configs.recommended,
            ...eslintPluginVue.configs["flat/recommended"]
        ],
        files: ["**/*.{ts,vue}"],
        languageOptions: {
            ecmaVersion: "latest",
            sourceType: "module",
            globals: globals.browser,
            parserOptions: {
                parser: typescriptEslint.parser
            }
        },
        rules: {
            "vue/attribute-hyphenation": [
                "error",
                "never",
                {
                    ignore: [],
                    ignoreTags: []
                }
            ],
            "vue/v-on-event-hyphenation": [
                "error",
                "never",
                {
                    ignore: [],
                    ignoreTags: []
                }
            ]
        }
    },
    eslintConfigPrettier
);
