<template>
    <v-carousel :height="height" hide-delimiters>
        <v-carousel-item
            v-for="(item,i) in images"
            :key="i"
            :src="item"
            reverse-transition="fade-transition"
            transition="fade-transition">
      
        </v-carousel-item>
    </v-carousel>
</template>

<script>
    export default {
         props:["images", "height"]
    }
</script>

<style lang="scss" scoped>

</style>