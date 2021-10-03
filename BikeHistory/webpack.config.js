const path = require('path');
const { VueLoaderPlugin } = require("vue-loader");
var webpack = require('webpack');
var featureFlags = new webpack.DefinePlugin({
  __VUE_OPTIONS_API__: true,
  __VUE_PROD_DEVTOOLS__: false
});

module.exports = {
    entry: './wwwroot/js/script.js',
    //mode: "development",
    mode: "production",
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

