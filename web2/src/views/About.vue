<template>
 <div class=""> 
    
   <v-row dense>
      
      <v-col
        v-for="n in 20"
        :key="n"
        cols=auto
        sm=4
      >
      <Productcard v-bind:product="items" v-bind:height="'250'"/>  
      </v-col>
    </v-row>
    <v-spacer></v-spacer>
    <div class="text-center">
    <v-pagination
      v-model="page"
      :length="6"
    ></v-pagination>
  </div></div>
   


</template>

<script>import Productcard from "../components/Productcard";
    export default {
        components: {
            Productcard,
        },
      data () {
      return {
        items: {id:"1345",
              title:"Nice Jeans",
              maker:{userid:"34252", name:"Superjeans", avatar:"https://randomuser.me/api/portraits/men/6.jpg"},
              sizes:["S","L", "XL"],
              description:"very nice jeans",
              tags:["Long", "Jeans", "skinny"],
              type:"Jeans",
              valuta: "$",
              delivery:[{country:"Germany", mindays:1, maxdays:2, deliveryprice:"3,25", price:"50,00"}],
              images: ["https://cdn.vuetifyjs.com/images/carousel/squirrel.jpg", "https://cdn.vuetifyjs.com/images/carousel/sky.jpg", "https://cdn.vuetifyjs.com/images/carousel/bird.jpg"],
              stars:"3,5",
              reviews:[{userid:"46856", username:"Frans", avatar:"https://randomuser.me/api/portraits/men/9.jpg", "stars":5, "title":"Amazing jeans", "review":"These are perfect", "date":"3-7-2017" }],
              designers:[{userid:"356743", name:"Franciesca", avatar:"https://randomuser.me/api/portraits/women/9.jpg"}],
              },
      }
    },
  }
</script>

<style lang="scss" scoped>

</style>