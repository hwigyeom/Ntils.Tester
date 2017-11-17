<template>
  <div class="message">
    <h1>Hello from Vue 2!</h1>
    <Message v-for="(msg, index) in messages" :message="msg" :key="index"/>
    <button @click="fetchMessages(lastFetchedMessageDate)">Fetch a message</button>
  </div>
</template>

<script src="./App.vue.js"></script>

<style lang="scss" scoped>
  .message {
    h1 {
      color: #0069d9;
    }
  }
</style>
