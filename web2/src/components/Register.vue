<template>
    <v-card>
        <v-card-title>
          <span class="headline">Register</span>
        </v-card-title>
        <v-card-text>
          <v-container>
            <v-row>
              <v-col cols="12">
                <v-text-field label="Full Name*"  v-model="displayName" required></v-text-field>
              </v-col>
              <v-col cols="12">
                <v-text-field label="Email*" :rules="emailRules" v-model="email" required></v-text-field>
              </v-col>
              <v-col cols="12">
                <v-text-field label="Password*" type="password" v-model="password" required></v-text-field>
              </v-col>
              <v-col cols="12">
                <v-file-input accept="image/*" label="Profilepicture*" v-model="profilepic" required></v-file-input>
              </v-col>
            </v-row>
          </v-container>
          <small>*indicates required field</small>
        </v-card-text>
        <v-card-actions>
          <v-spacer></v-spacer>
          <v-btn color="blue darken-1" text @click="dialog = false">Close</v-btn>
          <v-btn color="blue darken-1" text @click="register()">Register</v-btn>
        </v-card-actions>
      </v-card>
</template>

<script>
    export default {
        
    }
</script>

<style lang="scss" scoped>

</style>