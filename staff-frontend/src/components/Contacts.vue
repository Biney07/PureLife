<template>
  <div v-if="contacts.length" class="contact-list">
      <div @click="selectContact(contact)" v-for="contact in contacts" :key="contact.id" class="contact-row" :class="{ active: selectedContact && selectedContact.id === contact.id }">
        <img :src="contact.pictureUrl" alt="Contact Image" class="contact-image" />
        <div class="contact-name">{{ contact.emri }} {{ contact.mbiemri }}</div>
      </div>
    </div>
</template>

<script>
import { getPacientet } from "../staff-sdk/pacienti";

export default {
  data() {
    return {
        contacts: [],
        selectedContact: null,
        contactSelected: {
            recipientId: null
        }
    };
  },
  mounted() {
    this.fetchPatients();
  },
  methods: {
    async fetchPatients() {
        try {
            const response = await getPacientet();
            this.contacts = response.data
        } catch (err) {
            // eslint-disable-next-line no-console
            console.log(err)
        }
    },
    selectContact(contact) {

        this.selectedContact = contact
        this.contactSelected.recipientId = contact.uId

        this.$emit('fetchContactMessages', this.contactSelected)
    }
  }
}
</script>

<style scoped>
.contact-list {
  display: flex;
  gap: 30px;
  max-height: 200px;
  overflow-x: auto;
  padding: 10px;
  scrollbar-gutter: stable;
}

.contact-row {
  display: flex;
  flex-direction: column;
  align-items: center;
  text-align: center;
  width: fit-content; 
  border: 1px solid lightgray;
  padding: 5px 10px;
  border-radius: 8px;
  cursor: pointer;
  transition: box-shadow 0.1s ease-in;
}

.active{
  border: 2px solid black;
}

.contact-image {
  width: 80px;
  height: 80px;
  object-fit: cover;
  border-radius: 50%;
}

.contact-name {
  margin-top: 10px;
}
</style>