<template>
<v-app id="inspire">
    

    <v-app-bar
      app
      clipped-left
      color="red"
      dense
    >
      <v-row justify="center">
    <v-dialog v-model="dialog" persistent max-width="600px">
      <template v-slot:activator="{ on, attrs }">
        <v-btn
          color="primary"
          dark
          v-bind="attrs"
          v-on="on"
        >
          Open Dialog
        </v-btn>
      </template>
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
    </v-dialog>
  </v-row>
       <v-spacer></v-spacer>
      <v-row
        align="center"
        justify="center"
        style="max-width: 300px"
      >
        <v-text-field
          :append-icon-cb="() => {}"
          placeholder="Search..."
          single-line
          append-icon="mdi-magnify"
          color="white"
          hide-details
        ></v-text-field>
        
        
      </v-row>
      <v-btn
          color="pink"
          dark
          @click.stop="drawer = !drawer"
        >
          Toggle
      </v-btn>
    </v-app-bar>
    <v-navigation-drawer
      v-model="drawer"
      absolute
      right
      temporary
    >
      <v-list-item>
        <v-list-item-avatar>
          <v-img src="https://randomuser.me/api/portraits/men/78.jpg"></v-img>
        </v-list-item-avatar>

        <v-list-item-content>
          <v-list-item-title>John Leider</v-list-item-title>
        </v-list-item-content>
      </v-list-item>

      <v-divider></v-divider>

      <v-list dense>
        <v-list-item>
          <v-switch
        v-model="$vuetify.theme.dark"
        hide-details
        inset
        label="Dark Mode"
          ></v-switch>
        </v-list-item>
        <v-list-item>
          <v-btn
          color="primary"
          dark
          v-bind="attrs"
          v-on="on"
        >
          Open Dialog
        </v-btn>  
        </v-list-item>
      </v-list>
    </v-navigation-drawer>
    <v-main>
      <v-container >
          <router-view/>
      </v-container>
    </v-main>
    
  <v-bottom-navigation
    fixed :value="true"
    color="primary"
    horizontal
    scroll-threshold=300
    grow
  >
  
    <v-btn to="/about">
      <span>Recents</span>

      <v-icon>mdi-history</v-icon>
    </v-btn>

    <v-btn to="/seller">
      <span>Favorites</span>

      <v-icon>mdi-heart</v-icon>
    </v-btn>

    <v-btn>
      <span>Nearby</span>

      <v-icon>mdi-map-marker</v-icon>
    </v-btn>
  </v-bottom-navigation>
    
  </v-app>
    
</template>

<script>
import * as firebase from 'firebase/app' 
import 'firebase/auth'
import 'firebase/storage'
  export default {
    methods: {
        darkMode() {
            this.$vuetify.theme.dark = this.darkMode
        },

        register(){  
          const storageRef = firebase.storage().ref()
          firebase.auth().createUserWithEmailAndPassword(this.email, this.password).then(() => {
            const user = firebase.auth().currentUser;
            const profilePicRef = storageRef.child(`profilepictures/${user.uid}`)
            profilePicRef.put(this.profilepic).then(
            this.photoURL = profilePicRef.getDownloadURL().then(
            user.updateProfile({
            displayName: this.displayName,
            photoURL: this.photoURL
            }).then(
              this.dialog = false
            )))  
          })
        }
    },  
    props: {
      attrs: {
        type: Object,
        default: () => ({}),
      },
    },
    data: vm => ({
      initialDark: vm.$vuetify
        ? vm.$vuetify.theme.dark
        : false,
      darkMode: false,
      dialog: false,
      emailRules: [
        v => !!v || 'E-mail is required',
        v => /.+@.+\..+/.test(v) || 'E-mail must be valid',
      ],
      drawer: null,
      items: [
        { icon: 'mdi-trending-up', text: 'Most Popular', route: "about" },
        { icon: 'mdi-youtube-subscription', text: 'Subscriptions', route: "product" },
        { icon: 'mdi-history', text: 'History', route: "about" },
        { icon: 'mdi-playlist-play', text: 'Playlists', route: "seller" },
        { icon: 'mdi-clock', text: 'Watch Later', route: "home" },
      ],
      items2: [
        { picture: 28, text: 'Joi' },
        { picture: 38, text: 'Alo' },
        { picture: 48, text: 'Xfyui' },
        { picture: 58, text: 'fuyi' },
        { picture: 78, text: 'ryui' },
      ],
    }),

   
    beforeDestroy () {
      if (!this.$vuetify) return

      this.$vuetify.theme.dark = this.initialDark
    },
  
    created () {
      this.$vuetify.theme.dark = false
    },
  }
</script>
