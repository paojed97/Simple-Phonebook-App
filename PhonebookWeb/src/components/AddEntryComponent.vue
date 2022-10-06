<script setup>
import apiClient from "../services/PhonebookService";
import { ref } from "vue";

const props = defineProps({
  bookName: {
    type: String,
    required: true,
  },
});

const name = ref("");
const number = ref("");
const statusCode = ref(0);

function handleUpdate() {
  const phoneBookQuery = ref(props.bookName);
  let entryData = {
    name: name.value,
    Phone_Number: number.value,
  };

  apiClient.addEntry(phoneBookQuery.value, entryData).then((response) => {
    statusCode.value = response.status;
    console.log(response);
  });
}
</script>

<template>
  <div class="p-5">
    <form class="m-3" @submit.prevent="handleUpdate">
      <h3>Add New Entry</h3>
      <div class="mb-3">
        <label for="newEntryName" class="form-label">Name</label>
        <input
          type="text"
          class="form-control"
          id="newEntryName"
          name="newEntryName"
          v-model="name"
        />
      </div>
      <div class="mb-3">
        <label for="newEntryNumber" class="form-label">Phone Number</label>
        <input
          type="text"
          class="form-control"
          id="newEntryNumber"
          name="newEntryNumber"
          v-model="number"
        />
      </div>
      <input type="submit" class="btn btn-dark" value="Create" />
    </form>

    <div class="mt-5" v-if="statusCode === 204">
      <div class="alert alert-success" role="alert">
        Entry added! Please refresh the page.
      </div>
    </div>
  </div>
</template>
