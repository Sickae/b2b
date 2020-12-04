const RemovePlugin = require('remove-files-webpack-plugin');
const saveAssets = require('assets-webpack-plugin');
const path = require('path');
const webpack = require('webpack');

module.exports = function (env) {

    const distPath = path.resolve(__dirname, 'wwwroot', 'dist');

    env = env || {};
    var isProd = env.NODE_ENV === 'production';
    const useVersioning = true;

    var config = {
        mode: 'development',
        entry: {
            layout: './wwwroot/src/js/sites/layout.js',
        },
        output: {
            filename: useVersioning ? '[name].bundle.[hash:6].js' : '[name].bundle.js',
            path: distPath,
            publicPath: '/dist/'
        },
        optimization: {
            minimize: false,
            splitChunks: {
                chunks: 'all',
                maxAsyncRequests: 5,
                maxInitialRequests: 5,
            }
        },
        plugins: [
            new RemovePlugin({
                /**
                 * Before compilation permanently removes
                 * entire `./dist` folder.
                 */
                before: {
                    include: [
                        distPath
                    ]
                }
            }),

            new saveAssets({
                entrypoints: true,
                path: path.resolve(__dirname, 'wwwroot'),
            }), 
        ],
        module: {
            rules: [
                {
                    test: /\.m?js$/,
                    exclude: /(node_modules|bower_components)/,
                    use: {
                        loader: 'babel-loader',
                        options: {
                            presets: ['@babel/preset-env']
                        }
                    }
                },
                {
                    test: /\.s[ac]ss$/i,
                    use: [
                        // Creates `style` nodes from JS strings
                        'style-loader',
                        // Translates CSS into CommonJS
                        'css-loader',
                        // Compiles Sass to CSS
                        'sass-loader',
                    ],
                },
                {
                    test: /\.css$/,
                    use: [
                        'style-loader',
                        'css-loader',
                    ],
                },
                {
                    test: /\.(gif|png|jpe?g|svg)$/i,
                    use: [
                        'file-loader',
                        {
                            loader: 'image-webpack-loader',
                            options: {
                                mozjpeg: {
                                    progressive: true,
                                    quality: 65
                                },
                                // optipng.enabled: false will disable optipng
                                optipng: {
                                    enabled: false,
                                },
                                pngquant: {
                                    quality: [0.6, 0.9],
                                    speed: 1
                                },
                                gifsicle: {
                                    interlaced: false,
                                },
                                // the webp option will enable WEBP
                                webp: {
                                    quality: 75
                                }
                            }
                        },
                    ],
                },
                {
                    test: /\.(woff|woff2|eot|ttf|otf)$/,
                    use: [
                        'file-loader',
                    ],
                }
            ],
        },
    };

    // Alter config for prod environment
    if (isProd) {
        config.optimization.minimize = true;
        config.mode = 'production';
    }
    if (!isProd) {
        //config.devtool = "eval-source-map"; 'eval-cheap-source-map'
        config.devtool = "source-map";
    }

    return config;
};
