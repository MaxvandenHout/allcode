<template>
<div class="">
    <v-tabs
      v-model="tab"
      background-color="transparent"
      grow
    >
      <v-tab> Dashboard </v-tab>
      <v-tab> Profile </v-tab>
      <v-tab> Products </v-tab>
      <v-tab> Money </v-tab>
      <v-tab> New </v-tab>
    </v-tabs>

    <v-tabs-items v-model="tab">
      <v-tab-item >
          <Dashboard/>
      </v-tab-item>
      <v-tab-item >
        <Profile/>
      </v-tab-item>
       <v-tab-item >
         <products/>
      </v-tab-item>
      <v-tab-item >
        <money/>
      </v-tab-item>
      <v-tab-item >
        <NewAndUpdateProduct/>
      </v-tab-item>
    </v-tabs-items>
</div>
</template>
    

<script>
import Profile from '../components/Profile.vue';
import Dashboard from '../components/Dashboard.vue';
import Products from '../components/Products.vue';
import NewAndUpdateProduct from '../components/NewAndUpdateProduct';
import Money from '../components/Money.vue';
    
export default {
    components: {
        Profile, Money, NewAndUpdateProduct, Products, Dashboard
    },
    data: () => ({
      tab:null,
    }),
}
</script>

<style lang="scss" scoped>

</style>