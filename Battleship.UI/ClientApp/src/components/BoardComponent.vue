<template>
  <div class="board-grid">
    <div id="blank-cel"></div>
    <div v-for="col in cols" :key="col" :id="'cel-label-' + col">{{ col }}</div>
    <template v-for="row in 9" :key="row">
      <div :id="'cel-label-' + row">{{ row }}</div>
      <div v-for="col in 10" :key="col" :id="getCelId(row, col)"></div>
    </template>
  </div>
</template>
<script>
/* eslint-disable no-param-reassign */
export default {
  props: ['playerName', 'ships', 'opponentMoves'],
  data() {
    return {
      cols: ['A', 'B', 'C', 'D', 'E', 'F', 'G', 'H', 'I', 'J'],
    };
  },
  mounted() {
    this.ships.forEach((ship) => this.setShip(ship));

    if (this.opponentMoves) this.opponentMoves.forEach((move) => this.performMove(move));
  },
  methods: {
    setShip(ship) {
      const points = this.calculatePoints(ship.direction, ship.startPoint, ship.endpoint);
      const direction = ship.direction.toLowerCase();

      points.forEach((point, index) => {
        const cel = document.getElementById(`${this.playerName}-cel-${point}`);
        cel.innerHTML = '<div></div>';

        if (index === 0) {
          cel.classList.add(`ship-start-${direction}`);
        } else if (index === points.length - 1) {
          cel.classList.add(`ship-end-${direction}`);
        } else {
          cel.classList.add(`ship-middle-${direction}`);
        }
      });
    },
    performMove(move) {
      switch (move.action) {
        case 0:
          this.setMiss(move.attackedField);
          break;
        case 1:
          this.setBoom(move.attackedField);
          break;
        default:
          break;
      }
    },
    setBoom(celId) {
      const cel = document.getElementById(`${this.playerName}-cel-${celId}`);
      cel.classList.add('boom');
      cel.innerText = '💥';
    },
    setMiss(celId) {
      const cel = document.getElementById(`${this.playerName}-cel-${celId}`);
      cel.classList.add('miss');
      cel.innerText = '⚫';
    },
    getCelId(row, col) {
      const celId = parseInt(row, 10) * 10 + parseInt(col, 10) - 10;
      return `${this.playerName}-cel-${celId - 1}`;
    },
    calculatePoints(direction, startPoint, endPoint) {
      const points = [];

      while (startPoint <= endPoint) {
        points.push(startPoint);

        if (direction.toLowerCase() === 'horizontal') {
          startPoint += 1;
        } else {
          startPoint += 10;
        }
      }

      return points;
    },
  },
  computed: {},
};
</script>
<style scoped>
.board-grid {
  margin: 2vmin;
  display: grid;
  grid-template-rows: repeat(11, 4.6vmin);
  grid-template-columns: repeat(11, 4.6vmin);
}

.board-grid > div {
  font-size: 2vmin;
  border: 1px solid hsla(0, 0%, 0%, 0.2);
  display: flex;
  justify-content: center;
  align-items: center;
}

.miss {
  background: rgb(206, 206, 206);
}

.boom {
  background: rgb(255, 114, 114);
}
</style>
<style>
.ship-start-horizontal div {
  margin-left: auto;
  height: 80%;
  width: 90%;
  background: #eeac99;
  border-radius: 30px 0 0 30px;
  border-left: 1px solid hsla(0, 0%, 0%, 0.2);
  border-top: 1px solid hsla(0, 0%, 0%, 0.2);
  border-bottom: 1px solid hsla(0, 0%, 0%, 0.2);
}

.ship-middle-horizontal div {
  height: 80%;
  width: 100%;
  background: #eeac99;
  border-top: 1px solid hsla(0, 0%, 0%, 0.2);
  border-bottom: 1px solid hsla(0, 0%, 0%, 0.2);
}

.ship-end-horizontal div {
  margin-right: auto;
  height: 80%;
  width: 90%;
  background: #eeac99;
  border-radius: 0 30px 30px 0;
  border-right: 1px solid hsla(0, 0%, 0%, 0.2);
  border-top: 1px solid hsla(0, 0%, 0%, 0.2);
  border-bottom: 1px solid hsla(0, 0%, 0%, 0.2);
}

.ship-start-vertical div {
  margin-top: auto;
  height: 90%;
  width: 80%;
  background: #eeac99;
  border-radius: 30px 30px 0 0;
  border-left: 1px solid hsla(0, 0%, 0%, 0.2);
  border-top: 1px solid hsla(0, 0%, 0%, 0.2);
  border-right: 1px solid hsla(0, 0%, 0%, 0.2);
}

.ship-middle-vertical div {
  height: 100%;
  width: 80%;
  background: #eeac99;
  border-left: 1px solid hsla(0, 0%, 0%, 0.2);
  border-right: 1px solid hsla(0, 0%, 0%, 0.2);
}

.ship-end-vertical div {
  margin-bottom: auto;
  height: 90%;
  width: 80%;
  background: #eeac99;
  border-radius: 0 0 30px 30px;
  border-right: 1px solid hsla(0, 0%, 0%, 0.2);
  border-left: 1px solid hsla(0, 0%, 0%, 0.2);
  border-bottom: 1px solid hsla(0, 0%, 0%, 0.2);
}
</style>
