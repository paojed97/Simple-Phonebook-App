<script setup>
import { ref } from "vue";

const emit = defineEmits(["send-search"]);

const props = defineProps(["entries", "phonebookName"]);

const searchQuery = ref("");

function sendSearch() {
  const phoneBookQuery = ref(props.phonebookName);

  emit("send-search", phoneBookQuery.value, searchQuery.value);
}
</script>

<template>
  <div class="p-5 border-start border-end">
    <form class="m-3" @submit.prevent="sendSearch">
      <h3>Search</h3>
      <div class="mb-3">
        <input
          type="text"
          class="form-control"
          id="entryName"
          name="entryName"
          placeholder="Name"
          v-model="searchQuery"
        />
      </div>
      <input type="submit" class="btn btn-dark" value="Search" />
    </form>
    <div class="mt-5 text-center" v-if="!props.entries">
      You have no entries in this phonebook!
    </div>
    <div class="mt-5" v-else>
      <div
        class="card m-3"
        v-for="entry in props.entries"
        :key="entry.Phone_Number"
      >
        <div class="card-body">
          <h5 class="card-title">{{ entry.name }}</h5>
          <p class="card-text text-muted">{{ entry.Phone_Number }}</p>
        </div>
      </div>
    </div>
  </div>
</template>
