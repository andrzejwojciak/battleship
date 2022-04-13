<template>
  <div v-if="logs" class="logs-container">
    <template v-for="(log, index) in lastLogs" :key="log">
      <div :class="'log-level-' + index">
        <span class="nickname">{{ log.offensivePlayerName }}</span>
        {{ actionName(log) }}
      </div>
    </template>
  </div>
</template>
<script>
export default {
  props: ['logs'],
  data() {
    return {
      cols: ['A', 'B', 'C', 'D', 'E', 'F', 'G', 'H', 'I', 'J'],
    };
  },
  mounted() {},
  computed: {
    lastLogs() {
      return this.logs.slice().reverse().slice(0, 6);
    },
  },
  methods: {
    getFieldName(fieldNumber) {
      const tens = parseInt(fieldNumber / 10, 10) % 10;
      const ones = fieldNumber % 10;
      const label = this.cols[ones];
      return `${label}${tens + 1}`;
    },
    actionName(log) {
      switch (log.action) {
        case 0:
          return `took a shot at field ${this.getFieldName(log.attackedField)} and missed`;
        case 1:
          return `boomed another player's ship at field ${this.getFieldName(log.attackedField)}`;
        case 2:
          return 'won the game';
        default:
          return 'something went wrong';
      }
    },
  },
};
</script>
<style scoped>
.log-level-0 {
  opacity: 1;
}

.log-level-1 {
  opacity: 0.8;
}

.log-level-2 {
  opacity: 0.6;
}

.log-level-3 {
  opacity: 0.4;
}

.log-level-4 {
  opacity: 0.2;
}

.log-level-5 {
  opacity: 0.1;
}

.nickname {
  color: rgb(0, 80, 4);
}
</style>
