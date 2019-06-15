<template>
  <v-card>

    <v-card-title>
      <v-toolbar
        card
        color="grey lighten-3"
      >
          <v-icon>fas fa-car</v-icon>
        <v-toolbar-title>Local hotspots</v-toolbar-title>
      </v-toolbar>
    </v-card-title>
    <v-layout
      justify-space-between
      pa-3
    >
      <v-flex xs5>
        <v-treeview

                :open.sync="open"
                :active.sync="active"
                :load-children="onGetAll"
                :items="items"
          
                activatable
                selected-color="indigo"
                open-on-click
                return-object
                expand-icon="fas fa-caret-down"
                transition
                loading-icon="fas fa-spinner"

        >
            <template v-slot:prepend="{ item, active }">
                <v-icon v-if="(dataType[item.type])">
                    {{ dataType[item.type] }}
                </v-icon>
            </template>
        </v-treeview>
      </v-flex>
      <v-flex
        d-flex
        text-xs-center
      >
        <v-scroll-y-transition mode="out-in">
          <div
            v-if="!selected"
            class="title grey--text text--lighten-1 font-weight-light"
            style="align-self: center;"
          >
            Select Data
          </div>
          <v-card v-else
                  :key="selected.id"
                  class="pt-4 mx-auto"
                  flat
                  max-width="400">
              <v-card-text>

                  <h3 class="headline mb-2">
                      Model Name:{{ isSelected.modelName }}
                  </h3>
                  <div class="blue--text mb-2">
                      Plate Number:{{ isSelected.plate }}
                  </div>
                  <div class="blue--text subheading font-weight-bold">
                      Data type:{{ isSelected.type }}

                  </div>
                  <div>
                      <v-icon>
                          fas fa-car
                      </v-icon>
                  </div>
              </v-card-text>
              <v-card-actions>
                  <v-dialog v-model="dialog" persistent max-width="600px">
                      <template v-slot:activator="{ on }">
                          <v-btn color="primary" dark v-on="on">Edit Data</v-btn>
                      </template>
                      <v-card>
                          <v-card-title>
                              <span class="headline">Data Profile</span>
                          </v-card-title>
                          <v-card-text>
                              <v-container grid-list-md >
                                  <v-layout wrap>
                                      <v-flex xs12 sm6 md4>
                                          <v-text-field label="modelName*" :placeholder="isSelected.modelName" disabled></v-text-field>
                                      </v-flex>
                                      <v-flex xs12 sm6 md4>
                                          <v-text-field label="plate" v-model="isSelected.plate" :placeholder="selected.plate" hint="the car number"></v-text-field>
                                      </v-flex>
                                      <v-flex xs12 sm6 md4>
                                          <v-text-field label="data type*" :placeholder="isSelected.type" hint="the data type" required disabled></v-text-field>
                                      </v-flex>
                                      </v-layout>
                              </v-container>
                              <small>*indicates required field</small>
                          </v-card-text>
                          <v-card-actions>
                              <v-spacer></v-spacer>
                              <v-btn color="blue darken-1" flat
                                     :loading="isUpdate"
                                     :disabled="isUpdate"
                                     @click="onUpdate">Update</v-btn>
                              <v-btn color="blue darken-1" flat @click="dialog = false">Close</v-btn>
                          </v-card-actions>
                      </v-card>
                  </v-dialog>
                  <v-spacer></v-spacer>
                  <v-btn flat color="red">Delete</v-btn>
              </v-card-actions>
          </v-card>
        </v-scroll-y-transition>
      </v-flex>
    </v-layout>
  </v-card>
</template>

<script>

  import axios from 'axios'

  const avatars = [
    '?accessoriesType=Blank&avatarStyle=Circle&clotheColor=PastelGreen&clotheType=ShirtScoopNeck&eyeType=Wink&eyebrowType=UnibrowNatural&facialHairColor=Black&facialHairType=MoustacheMagnum&hairColor=Platinum&mouthType=Concerned&skinColor=Tanned&topType=Turban',
    '?accessoriesType=Sunglasses&avatarStyle=Circle&clotheColor=Gray02&clotheType=ShirtScoopNeck&eyeType=EyeRoll&eyebrowType=RaisedExcited&facialHairColor=Red&facialHairType=BeardMagestic&hairColor=Red&hatColor=White&mouthType=Twinkle&skinColor=DarkBrown&topType=LongHairBun',
    '?accessoriesType=Prescription02&avatarStyle=Circle&clotheColor=Black&clotheType=ShirtVNeck&eyeType=Surprised&eyebrowType=Angry&facialHairColor=Blonde&facialHairType=Blank&hairColor=Blonde&hatColor=PastelOrange&mouthType=Smile&skinColor=Black&topType=LongHairNotTooLong',
    '?accessoriesType=Round&avatarStyle=Circle&clotheColor=PastelOrange&clotheType=Overall&eyeType=Close&eyebrowType=AngryNatural&facialHairColor=Blonde&facialHairType=Blank&graphicType=Pizza&hairColor=Black&hatColor=PastelBlue&mouthType=Serious&skinColor=Light&topType=LongHairBigHair',
    '?accessoriesType=Kurt&avatarStyle=Circle&clotheColor=Gray01&clotheType=BlazerShirt&eyeType=Surprised&eyebrowType=Default&facialHairColor=Red&facialHairType=Blank&graphicType=Selena&hairColor=Red&hatColor=Blue02&mouthType=Twinkle&skinColor=Pale&topType=LongHairCurly'
  ]

  const pause = ms => new Promise(resolve => setTimeout(resolve, ms))

  export default {
    data: () => ({
      countries: [], // axios puts here
      countriesForItems: [], // all #cars and #companies are replace with #children
          tree: [{name: 'start'}], // for model bind
          dataType: {
              car: 'fas fa-car',
              country: 'fas fa-flag',
              company:'fas fa-copyright'

          },
      active: [],
      avatar: null,
          cars: [],
          companies: [],
          dialog: false,//for the edit dialog
          isUpdate:false,//for update
      open: ['public'],

      isSelected: {}
    }),

    computed: {
        items() {       
            console.log("items=== update")
        return [
            {
            name: 'Data',
            children:this.countriesForItems
          }
        ]
      },
      selected () {
          if (!this.active.length) {
              return undefined
          }
          console.log("isSelected1===" + this.isSelected.modelName)
          
          this.isSelected = this.cars.find(car => car.id == this.active[0].id)
          var temp = this.isSelected
          console.log("isSelected===" + temp.modelName)
          if (temp.plate == null) {
              temp.plate = temp.modelName +"-000"
          }
        return temp
      }
    },
    // watch the countries and trensform it to countriesForItems
    watch: {
        countries(val) {
            console.log("update watch countries ")
        var listForReplace = JSON.stringify(val)
        listForReplace = listForReplace.replace(/"cars":/g, '"children":').replace(/"companies":/g, '"children":').replace(/"modelName":/g, '"name":')
            this.countriesForItems = JSON.parse(listForReplace)

            //create group for companies
            this.companies = val.reduce((acc, cur) => {
                return [...acc, ...cur.companies]
            }, []).sort()
                        //create group for cars for func(find)
            this.cars = this.companies.reduce((acc, cur) => {
                return [...acc, ...cur.cars]
            }, []).sort()
      },
        isSelected(val) {


        }
    },

    methods: {

      randomAvatar () {
        this.avatar = avatars[Math.floor(Math.random() * avatars.length)]
      },
      async onGetAll () {
        console.log('in onGetAll')
        this.isUpdate = true
        console.log('in onGetAll ==> axios')
        await axios.get('https://localhost:44331/api/SampleData/GetCountriesAsync')
          .then(response => {
            console.log('response==>' + response + '==' + JSON.stringify(response.data))
            // this.countries.push(response.data)
            this.countries = response.data
            // console.log("response==>" + this.countries.push(response.data) + "==" + response.data)
          })
          .catch(err => console.log(err))

        this.isUpdate = false
      },
        async onUpdate() {
            this.isUpdate = true
            console.log('in update')

            var tempCheck = this.isSelected
            console.log('in update ==> axios')
            if (!(this.isSelected.plate === this.isSelected.modelName +"-000")) {


                await axios.post('https://localhost:44331/api/SampleData/UpdateCarAsync', this.isSelected).then(response => {
                    if (this.isSelected === response.data) {
                        console.log('good update')
                    } else {
                        console.log(' not good update  ' + response.data + '==' + response + '==' + response.data.value + '==' + tempCheck)
                    }
                    this.isSelected = response.data
                })
                    .catch(err => {
                        console.log(err)
                    })
            }
            else {
                console.log('no update.....notning new')
                
                console.log('no update == ' + this.isSelected.plate)
            }
        this.isUpdate = false
      }
    }
  }
</script>

<style></style>
