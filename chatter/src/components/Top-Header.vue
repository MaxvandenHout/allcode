<template>
    <div>
        Logged in
        <span v-if="loggedin">Yes</span>
        <span v-else>no</span>
        <div class="">
            <button @click="signOut">Sign out</button>
        </div>
    </div>
</template>

<script>
    import * as firebase from 'firebase/app' 
    import 'firebase/auth'
    export default {
        created () {
           firebase.auth().onAuthStateChanged(
               user => {
                   if (user) {
                       this.loggedin = true
                   }else {
                       this.loggedin = false
                   }
               }
           ) ;
        },
         methods: {
         async signOut() {
             try {
                 const data = firebase.auth().signOut()
                 this.$router.replace("/login")

             } catch (error) {
                 console.log(error)
             }
           
             }
         },
        
        data() {
            return {
                loggedin: false
            }
        },
    }
</script>

<style lang="scss" scoped>

</style>