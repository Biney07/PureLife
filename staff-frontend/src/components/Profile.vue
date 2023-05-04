<template>
  <div v-if="userData" class="profile-container">
    <div class="profile-left">
      <div class="profile-image-container">
        <img :src="userData.pictureUrl" alt="User Profile Image" class="profile-image" />
        <div class="name-birthday">
          <p>{{userData.emri}} {{userData.mbiemri}}</p>
          <small>Date of Birth: {{new Date(userData.dataLindjes).toLocaleDateString()}}</small>
        </div>
      </div>
      <div class="profile-info">
        <p>License Number: {{userData.nrLincences}}</p>
        <p>Specializes in: {{userData.lemia}}</p>
      </div>
    </div>
    <div class="profile-right">
      <div class="profile-form">
        <div class="profile-form-header">
          <p>Personal Information</p>
          <button type="submit" @click="editProfile" class="profile-save-button">Save Changes</button>
        </div>
        <hr>
        <div class="profile-form-content">
          <div class="profile-image-container pl-0 mb-4">
            <img :src="userData.pictureUrl" alt="User Profile Image" class="profile-image" />
            <p class="profile-save-button">Change Image</p>
            <input type="file" ref="fileInput" style="display:none">
          </div>
          <form>
            <div class="form-outline">
              <label class="form-label font-weight-normal" for="form1Example1">Name:</label>
              <input v-model="userData.emri" class="form-control" placeholder="Your name" />
            </div>

            <!-- Password input -->
            <div class="form-outline">
              <label class="form-label font-weight-normal" for="form1Example2">Last Name:</label>
              <input v-model="userData.mbiemri" placeholder="Your last name" class="form-control" />
            </div>

            <div class="form-outline">
              <label class="form-label font-weight-normal" for="form1Example2">Date of birth:</label>
              <input type="date" v-model="userData.dataLindjes" placeholder="Date of birth" class="form-control" />
            </div>

            <div>
              <label class="form-label font-weight-normal" for="nationality">Gender</label>
              <select id="nationality" v-model="userData.gjinia" class="form-control">
                <option value="" disabled selected>Select your gender</option>
                <option value="M">M</option>
                <option value="F">F</option>
              </select>
            </div>

            <div class="form-outline">
              <label class="form-label font-weight-normal" for="form1Example1">Email:</label>
              <input v-model="userData.email" type="email" class="form-control" placeholder="Your personal email" />
            </div>

            <div class="form-outline">
              <label class="form-label font-weight-normal" for="form1Example2">Phone Number:</label>
              <input v-model="userData.nrTel" type="number" placeholder="Your phone number" class="form-control" />
            </div>

            <div class="form-outline">
              <label class="form-label font-weight-normal" for="form1Example2">ID Number:</label>
              <input v-model="userData.nrLeternjoftimit" type="number" placeholder="Your ID number" class="form-control" />
            </div>

            <div class="form-outline">
              <label class="form-label font-weight-normal" for="form1Example2">License Number:</label>
              <input v-model="userData.nrLincences" type="number" placeholder="Your license number" class="form-control" />
            </div>

            <div>
              <label class="form-label font-weight-normal" for="country">Country</label>
              <select id="country" v-model="userData.shtetiId" class="form-control">
                <option value="" disabled selected>Select your country</option>
                <option v-for="country in countries" :value="country.id" :key="country.id">{{ country.emri }}</option>
              </select>
            </div>

            <div class="form-outline">
              <label class="form-label font-weight-normal" for="form1Example2">City:</label>
              <input v-model="userData.qyteti" placeholder="City" class="form-control" />
            </div>

            <div>
              <label class="form-label font-weight-normal" for="nationality">Nationality</label>
              <select id="nationality" v-model="userData.nacionalitetiId" class="form-control">
                <option value="" disabled selected>Select your nationality</option>
                <option v-for="nationality in nationalities" :value="nationality.id" :key="nationality.id">{{ nationality.emri }}</option>
              </select>
            </div>

          </form>
        </div>
      </div>
    </div>
  </div>
</template>

<script>
import {getAllNationalities, getAllCountries} from "../staff-sdk/nacionaliteti"
import {fetchCurrentUser} from "../staff-sdk/user"
export default {
  data() {
    return {
      userData: null,
      nationalities: null,
      countries: null
    }
  },
  async mounted() {
    await this.fetchNationalities()
    await this.fetchCountries()
    await this.getCurrentUser();
  },
  methods: {
    async fetchNationalities() {
      try {
        const response = await getAllNationalities()
        this.nationalities = response?.data
      } catch (err) {
        // eslint-disable-next-line no-console
        console.log(err)
      }
    },
    async fetchCountries() {
      try {
        const response = await getAllCountries()
        this.countries = response?.data
      } catch (err) {
        // eslint-disable-next-line no-console
        console.log(err)
      }
    },
    async editProfile(){
      // eslint-disable-next-line no-console
        this.userData.modifiedFrom = this.userData.emri && this.userData.mbiemri ? `${this.userData.emri} ${this.userData.mbiemri}` : "Unknown";
        this.userData.modifiedDate = new Date().toISOString();
        try {
          this.$store.dispatch('editUserProfile', this.userData)
        } catch (err) {
          // eslint-disable-next-line no-console
          console.log(err)
        }
    },
    async getCurrentUser(){
      try {
        const response = await fetchCurrentUser(this.$store.state.authenticate.user.data.id)
        // eslint-disable-next-line no-console
        this.userData = response?.data
      } catch (err) {
        // eslint-disable-next-line no-console
        console.log(err)
      }
    }
  }

}
</script>

<style scoped>
.profile-container {
  display: flex;
  height: 100%;
  gap: 20px;
}

.profile-left {
  width: 30%;
  background-color: #fff;
  border-radius: 6px;
  box-shadow: none;
  height: fit-content;
}

.profile-right {
  width: 70%;
  background-color: #fff;
  border-radius: 6px;
  box-shadow: none;
}

.profile-image-container {
  display: flex;
  justify-content: flex-start;
  align-items: flex-start;
  height: 100px;
  margin-top: 4%;
  padding: 0 20px;
}

.name-birthday p{
  padding: 0;
  margin: 0;
  font-weight: 500;
  font-size: 18px;
}

.name-birthday small{
  font-weight: 500;
}

.profile-image {
  width: 100px;
  height: 100px;
  border-radius: 10%;
  object-fit: cover;
  margin-right: 4%;
}

.profile-info {
  padding: 20px;
}

.profile-form {
  padding: 20px;
}

.profile-form-header {
  display: flex;
  justify-content: space-between;
  align-items: center;
}

.profile-form-header p {
  font-size: 28px;
  padding: 0;
  margin: 0;
}

.profile-save-button {
  background-color: #1c80c8;
  color: #fff;
  border: none;
  border-radius: 5px;
  padding: 10px 20px;
  cursor: pointer;
}

.profile-save-button:focus {
  border: none;
  outline: none;
  box-shadow: none;
}

.profile-form-content {
  margin-top: 20px;
}

form {
  display: flex;
  flex-direction: column;
}

label {
  margin-bottom: 5px;
}

input.form-control, select.form-control {
  border: none;
  background: #f3f6f9;
  border-radius: 8px;
  height: 40px;
  transition: background-color 0.3s;
  padding: 0 15px;
  margin-bottom: 20px;
}

.form-control:focus {
  border: none;
  outline: none;
  background: #ebedf3;
  box-shadow: none;
  transition: background-color 0.3s;
  padding: 0 15px;
}

select.form-control option {
  border: none;
  background: #f3f6f9;
  border-radius: 8px;
  transition: background-color 0.3s;
  padding: 0 15px;
}

select.form-control option:hover {
  background: #ebedf3;
}

@media (max-width: 980px) {
  .profile-container {
    flex-direction: column;
  }

  .profile-left {
    width: 100%;
    margin-bottom: 20px;
  }

  .profile-image-container {
    height: 60px;
  }

  .name-birthday p{
    font-size: 18px;
  }

  .profile-image {
    width: 50px;
    height: 50px;
    border-radius: 10%;
    object-fit: cover;
    margin-right: 4%;
  }

  .profile-info{
    font-size: 16px;
  }

  .profile-right {
    width: 100%;
  }

  .profile-save-button {
    font-size: 14px;
    padding: 5px 10px;
  }

  .profile-form-header p {
    font-size: 16px;
  }
}

@media (max-width: 500px) {
  .profile-save-button {
    font-size: 8px;
    padding: 5px;
  }

  .profile-form-header p {
    font-size: 12px;
  }

  .profile-form-content{
    font-size: 12px;
  }

  .profile-info{
    font-size: 12px;
  }
  
  .name-birthday p{
    font-weight: 400;
    font-size: 14px;
  }

  .name-birthday small{
    font-weight: 400;
    font-size: 12px;
  }

  input.form-control {
    font-size: 12px;
  }
}
</style>
