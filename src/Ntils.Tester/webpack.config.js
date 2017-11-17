const path = require('path');
const webpack = require('webpack');
const ExtractTextPlugin = require('extract-text-webpack-plugin');

const bundleOutputDir = './wwwroot/dist';

module.exports = (env) => {
  const isDevBuild = !(env && env.prod);

  return [ {
    stats: { modules: false },
    context: __dirname,
    resolve: { extensions: [ '.js' ] },
    entry: { 'main': './ClientApp/app.js' },

    output: {
      path: path.resolve(__dirname, './wwwroot'),
      publicPath: '/dist/',
      filename: isDevBuild ? 'dist/[name].js' : 'dist/[name].min.js'
    },

    module: {
      rules: [
        {
          test: /\.vue$/,
          include: /ClientApp/,
          loader: 'vue-loader'
        },
        {
          test: /\.js$/,
          include: /ClientApp/,
          loader: 'babel-loader'
        },
        {
          test: /\.s[a|c]ss$/,
          include: /ClientApp\/scss/,
          use: isDevBuild ? [
            { loader: 'style-loader' },
            {
              loader: 'css-loader',
              options: { minimize: true }
            },
            {
              loader: 'postcss-loader',
              options: {
                plugins: function () {
                  return [
                    require('autoprefixer')
                  ];
                }
              }
            },
            { loader: 'sass-loader' }
          ] : ExtractTextPlugin.extract({
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
        // {
        //   test: /\.(png|jpg|jpeg|gif|svg)$/,
        //   include: /ClientApp\/images/,
        //   use: 'url-loader?limit=25000'
        // },
        // {
        //   test: /\.(eot|svg|ttf|woff|woff2)$/,
        //   include: /ClientApp/,
        //   use: 'file-loader?name=wwwroot/fonts/[name].[ext]'
        // }
      ]
    },
    plugins: [
      new webpack.DefinePlugin({
        'process.env': {
          NODE_ENV: JSON.stringify(isDevBuild ? 'development' : 'production')
        }
      }),
      new webpack.DllReferencePlugin({
        context: __dirname,
        manifest: require(isDevBuild ?
          `${bundleOutputDir}/vendor-manifest.json` : `${bundleOutputDir}/vendor.min-manifest.json`)
      }),
      new webpack.SourceMapDevToolPlugin({
        filename: '[file].map',
        moduleFilenameTemplate: path.relative(bundleOutputDir, '[resourcePath]')
      })
    ].concat(isDevBuild ? [] : [
      new webpack.BannerPlugin({
        banner: `Ntils Tester v0.1
Copyright Hwigyeom Noh(whistle96@gmail.com)`
      }),
      new webpack.optimize.UglifyJsPlugin({ sourceMap: true }),
      new ExtractTextPlugin('css/site.min.css')
    ])
  } ];
};