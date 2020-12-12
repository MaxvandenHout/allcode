<template>
    <div>
           <div class="error">{{error.message}}</div>
       <form @submit.prevent="pressed">
     
           <div class="email">
               <input type="email" v-model="email" placeholder="email">
           </div>
           <div class="password">
               <input type="password" v-model="password" placeholder="password">
           </div>
           <button type="submit">Login</button>
           
       </form>
    </div>
</template>

<script>
import * as firebase from 'firebase/app' 
import 'firebase/auth'
    export default {
        methods: {
            async pressed() {
                try {
                  const val = firebase.auth().signInWithEmailAndPassword(this.email, this.password) 
                  console.log(val)
                  this.$router.replace("/chatter")
                } catch (error) {
                    console.log(error)
                }
                
            }
        },
        data() {
            return {
                error: "",
                email:"",
                password:""
            }
        },
    }
</script>

<style lang="scss" scoped>

</style>