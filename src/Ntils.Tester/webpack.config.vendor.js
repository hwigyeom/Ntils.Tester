const path = require('path');
const webpack = require('webpack');
const ExtractTextPlugin = require('extract-text-webpack-plugin');

module.exports = (env) => {
  const isDevBuild = !(env && env.prod);
  const extractCSS = new ExtractTextPlugin(isDevBuild ? 'css/ntils-bootstrap.css' : 'css/ntils-bootstrap.min.css');

  return [{
    stats: { modules: false },
    resolve: { extensions: [ '.js' ] },
    entry: {
      vendor: [
        'bootstrap',
        './ClientApp/scss/ntils-bootstrap.scss',
        'jquery',
        'vue',
        'vue-router',
        'vuex'
      ]
    },
    module: {
      rules: [
        {
          test: /\.s[a|c]ss(\?|$)/,
          use: extractCSS.extract({
            use: [
              {
                loader: 'css-loader',
                options: {
                  sourceMap: !isDevBuild,
                  minimize: !isDevBuild
                }
              },
              {
                loader: 'postcss-loader',
                options: {
                  sourceMap: !isDevBuild,
                  plugins: function () {
                    return [
                      require('autoprefixer')
                    ];
                  }
                }
              },
              {
                loader: 'sass-loader',
                options: {
                  sourceMap: !isDevBuild
                }
              }
            ],
            publicPath: '/css/'
          })
        },
        {
          test: /\.(png|woff|woff2|eot|ttf|svg)(\?|$)/,
          use: 'url-loader?limit=100000'
        }
      ]
    },
    devtool: 'source-map',
    output: {
      path: path.join(__dirname, 'wwwroot'),
      publicPath: '/dist/',
      filename: isDevBuild ? 'dist/[name].js' : 'dist/[name].min.js',
      library: '[name]_[hash]'
    },
    plugins: [
      extractCSS,
      new webpack.ProvidePlugin({ $: 'jquery', jQuery: 'jquery', }),
      new webpack.DefinePlugin({
        'process.env.NODE_ENV': isDevBuild ? '"development"' : '"production"'
      }),
      new webpack.DllPlugin({
        path: path.join(__dirname, 'wwwroot', 'dist', isDevBuild ? '[name]-manifest.json' : '[name].min-manifest.json'),
        name: '[name]_[hash]'
      })
    ].concat(isDevBuild ? []: [
      new webpack.optimize.UglifyJsPlugin({ sourceMap: true })
    ])
  }];
};