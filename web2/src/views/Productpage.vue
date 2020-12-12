<template>
    <div>
        <v-row>
            <v-col cols="6">
               
                   
                        <Carousel v-bind:images="images" />
                        
                    
            </v-col>
             <v-col cols="6">
                <v-card>
                    
                       <h1>{{title}}</h1>

                    <v-list subheader>
                        <v-subheader>Designers</v-subheader>

                    <v-list-item
                        v-for="item in designers"
                        :key="item.name"
                    
                    >
                        <v-list-item-avatar>
                        <v-img :src="item.avatar"></v-img>
                        </v-list-item-avatar>

                        <v-list-item-content>
                        <v-list-item-title v-text="item.name"></v-list-item-title>
                        </v-list-item-content>

                    
                    </v-list-item>
                    </v-list>
                    <v-rating :value="stars" readonly="readonly"></v-rating>
                  
               
                   
                </v-card>
            </v-col>
        
            <v-col cols="6">
                <v-card > 
                        <v-card-text>
                             {{description}}
                        </v-card-text>
            
                        
                    
                    </v-card>
            </v-col>
         </v-row>           
            
            
        <Reviews v-bind:reviews="reviews"/>
        

    </div>
</template>

<script>
import Carousel from "../components/Carousel.vue";
import Reviews from "../components/Reviews"
    export default {
        components: {
            Carousel, Reviews,
        },
    
        data() {
            return {
                id:"1345",
                title:"Nice Jeans",
                maker:{userid:"34252", name:"Superjeans", avatar:"https://randomuser.me/api/portraits/men/6.jpg"},
                sizes:["S","L", "XL"],
                description:"very nice jeans",
                tags:["Long", "Jeans", "skinny"],
                type:"Jeans",
                valuta: "$",
                delivery:[{country:"Germany", mindays:1, maxdays:2, deliveryprice:"3,25", price:"50,00"}],
                images: ["https://cdn.vuetifyjs.com/images/carousel/squirrel.jpg", "https://cdn.vuetifyjs.com/images/carousel/sky.jpg", "https://cdn.vuetifyjs.com/images/carousel/bird.jpg"],
                stars:3,
                reviews:[{userid:"46856", username:"Frans", avatar:"https://randomuser.me/api/portraits/men/9.jpg", "stars":5, "title":"Amazing jeans", "review":"These are perfect", "date":"3-7-2017" }],
                designers:[{userid:"356743", name:"Franciesca", avatar:"https://randomuser.me/api/portraits/women/45.jpg"}],
                
            }
        },
    }
</script>

<style lang="scss" scoped>

</style>