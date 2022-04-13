import axios from 'axios';

const apiClient = axios.create({
  baseURL: 'https://localhost:7284',
  withCredentials: false,
  headers: {
    Accept: 'application/json',
    'Content-Type': 'application/json',
  },
});

export default {
  createGame(player1Name, player2Name) {
    return apiClient.post('/games', { player1Name, player2Name });
  },
  getGameStateById(gameId) {
    return apiClient.get(`/games/${gameId}`);
  },
  doRandomMove(gameId) {
    return apiClient.post(`/games/${gameId}/random-move`);
  },
};
