const { defineConfig } = require('@vue/cli-service');

module.exports = defineConfig({
  transpileDependencies: true,
  devServer: { port: 44454, https: false, host: 'localhost' },
});
