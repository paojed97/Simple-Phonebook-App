<script setup>
import apiClient from "../services/PhonebookService";
import { ref } from "vue";

defineProps(["phoneBooks"]);

const newPhoneBook = ref("");
const statusCode = ref(0);

function handleCreate() {
  let newData = {
    name: newPhoneBook.value,
    entries: [],
  };

  apiClient.createPhonebook(newData).then((response) => {
    statusCode.value = response.status;
    console.log(response);
  });
}
</script>

<template>
  <div class="p-5">
    <div class="mt-5" v-if="statusCode === 201">
      <div class="alert alert-success" role="alert">
        Phonebook added! Please refresh the page.
      </div>
    </div>

    <form class="m-3" @submit.prevent="handleCreate">
      <h3>Create new Phonebook</h3>
      <div class="mb-3">
        <label for="phonebookName" class="form-label">Phonebook Name</label>
        <input
          type="text"
          class="form-control"
          id="phonebookName"
          name="phonebookName"
          placeholder="New Phonebook"
          v-model="newPhoneBook"
        />
      </div>
      <input type="submit" class="btn btn-dark" value="Create" />
    </form>
    <div class="mt-5" v-if="!phoneBooks">You have no phonebooks!</div>
    <div class="mt-5" v-else>
      <div
        class="card text-center m-3"
        v-for="phoneBook in phoneBooks"
        :key="phoneBook.id"
      >
        <div class="card-body" @click="$emit('get-phonebook', phoneBook.name)">
          <h5 class="card-title">{{ phoneBook.name }}</h5>
          <p class="card-text text-muted">
            {{ phoneBook.entries.length }} contact(s)
          </p>
        </div>
      </div>
    </div>
  </div>
</template>
