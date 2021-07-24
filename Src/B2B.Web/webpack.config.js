const RemovePlugin = require('remove-files-webpack-plugin');
const saveAssets = require('assets-webpack-plugin');
const path = require('path');
const webpack = require('webpack');

module.exports = function (env) {

    const distPath = path.resolve(__dirname, 'wwwroot', 'dist');

    env = env || {};
    let isProd = env.NODE_ENV === 'production';

    let config = {
        mode: 'development',
        entry: {
            layout: './wwwroot/src/ts/sites/layout.ts',
            login: './wwwroot/src/styles/sites/login.sass',
        },
        devtool: 'inline-source-map',
        output: {
            filename: '[name].bundle.[contenthash:6].js',
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
                    test: /\.tsx?$/,
                    use: 'ts-loader',
                    exclude: /node_modules/,
                },
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
        resolve: {
            extensions: ['.tsx', '.ts', '.js'],
        },
    };

    // Alter config for prod environment
    if (isProd) {
        config.optimization.minimize = true;
        config.mode = 'production';
    }

    return config;
};
