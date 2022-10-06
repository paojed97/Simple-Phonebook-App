<script setup>
import PhoneBookComponent from "../components/PhoneBookComponent.vue";
import EntryListComponent from "../components/EntryListComponent.vue";
import AddEntryComponent from "../components/AddEntryComponent.vue";
import apiClient from "../services/PhonebookService";
import { onMounted, ref } from "vue";

const entries = ref([]);
const phoneBooks = ref([]);
const phoneBookName = ref("");

function getCurrentPhoneBook(phonebookName) {
  apiClient.getPhoneBook(phonebookName).then((response) => {
    phoneBookName.value = response.data.name;
    entries.value = response.data.entries;
    console.log(entries.value, phoneBookName.value);
  });
}

function handleSearch(phonebookQuery, searchQuery) {
  apiClient.searchEntries(phonebookQuery, searchQuery).then((response) => {
    entries.value = response.data;
    console.log(response.data);
  });
}

onMounted(() => {
  apiClient.getPhonebooks().then((response) => {
    phoneBooks.value = response.data;
    console.log(phoneBooks.value);
  });
});
</script>

<template>
  <main class="row row-cols-1 row-cols-md-3">
    <PhoneBookComponent
      :phone-books="phoneBooks"
      @get-phonebook="getCurrentPhoneBook"
    />
    <EntryListComponent
      :entries="entries"
      :phonebook-name="phoneBookName"
      @send-search="handleSearch"
    />
    <AddEntryComponent :book-name="phoneBookName" />
  </main>
</template>

<style scoped>
main {
  max-width: 100%;
  max-height: 100%;
}
</style>
