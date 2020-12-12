<template>
  <div>
    <div v-for="item in users" :key="item.username">
      {{ item.username }}
    </div>
  </div>
</template>

<script>
import axios from 'axios';
  export default {
    data() {
      return {
        users: []
      }
    },
    created () {
      axios.get("http://node-mongo:3000/users/").then(data => {
        console.log(data)
        this.users = data
      });
    },
  }
</script>

<style lang="scss" scoped>

</style>