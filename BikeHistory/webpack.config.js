const path = require('path');
const { VueLoaderPlugin } = require("vue-loader");
const webpack = require('webpack');
const featureFlags = new webpack.DefinePlugin({
    __VUE_OPTIONS_API__: true,
    __VUE_PROD_DEVTOOLS__: false
});

module.exports = {
    entry: './wwwroot/js/script.js',
    mode: "development",
    //mode: "production",
    module: {
        rules: [
            {
                test: /\.vue$/,
                loader: 'vue-loader'
            }
        ]
    },
    plugins: [
        new VueLoaderPlugin(), featureFlags
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

