const path = require('path');
const { VueLoaderPlugin } = require("vue-loader");

module.exports = {
    entry: './wwwroot/js/script.js',
    mode: "development",
    module: {
        rules: [
        // ... other rules
                {
                    test: /\.vue$/,
                    loader: 'vue-loader'
                }
            ]
        },
        plugins: [
        // make sure to include the plugin!
        new VueLoaderPlugin()
        ],
    resolve: {
        alias: {
            vue: 'vue/dist/vue.esm-bundler.js'
        }
    },
    output: {
        filename: 'main.js',
        path: path.resolve(__dirname, 'wwwroot/js/'),
  },
};
