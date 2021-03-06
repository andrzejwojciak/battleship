import { createRouter, createWebHistory } from 'vue-router';
import HomeView from '../views/HomeView.vue';
import GameView from '../views/GameView.vue';
import ConnectionErrorView from '../views/ConnectionErrorView.vue';
import NotFoundView from '../views/NotFoundView.vue';

const routes = [
  {
    path: '/',
    name: 'home',
    component: HomeView,
  },
  {
    path: '/game/:gameId',
    name: 'game',
    component: GameView,
  },
  {
    path: '/connection-error',
    name: 'connection-error',
    component: ConnectionErrorView,
  },
  {
    path: '/not-found',
    name: 'not-found',
    component: NotFoundView,
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
