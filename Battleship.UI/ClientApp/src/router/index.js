import { createRouter, createWebHistory } from 'vue-router';
import HomeView from '../views/HomeView.vue';
import ConnectionErrorView from '../views/ConnectionErrorView.vue';

const routes = [
  {
    path: '/',
    name: 'home',
    component: HomeView,
  },
  {
    path: '/connection-error',
    name: 'connection-error',
    component: ConnectionErrorView,
  },
  {
    path: '/:catchAll(.*)',
    redirect: () => ({ path: '/' }),
  },
];

const router = createRouter({
  history: createWebHistory(process.env.BASE_URL),
  routes,
});

export default router;
