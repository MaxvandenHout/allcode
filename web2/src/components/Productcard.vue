<template>
    <div>
        <v-card
          class="mx-auto"
          dark=true
          outlined
          
          
        >
        <v-list-item>
      <v-list-item-avatar color="grey">
         <img
                :src="product.maker.avatar"
                alt=""
              >
      </v-list-item-avatar>
      <v-list-item-content>
        <v-list-item-title class="headline">{{product.title}}</v-list-item-title>
        <v-list-item-subtitle>by {{product.maker.name}}</v-list-item-subtitle>
      </v-list-item-content>
    </v-list-item>
       <Carousel v-bind:images="product.images" v-bind:height="'250'"/>
  <v-card-subtitle class="pb-0">Number 10</v-card-subtitle>

    <v-card-text class="text--primary">
      <div>Whitehaven Beach</div>

      <div>Whitsunday Island, Whitsunday Islands</div>
    </v-card-text>

    <v-card-actions>
      <v-btn
        color="orange"
        text
      >
        Share
      </v-btn>

      <v-btn
        color="orange"
        text
      >
        Explore
      </v-btn>
    </v-card-actions>
        </v-card>
    </div>
</template>

<script>
import Carousel from "./Carousel";

export default {
    components: {
        Carousel,
    },
    props: ["product"]
}

</script>

<style lang="scss" scoped>

</style>