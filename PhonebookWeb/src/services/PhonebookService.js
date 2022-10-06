import axios from "axios";

const apiClient = axios.create({
  baseURL: "https://localhost:7170/api/Phonebooks",
});

export default {
  getPhonebooks() {
    return apiClient.get("/");
  },
  getPhoneBook(name) {
    return apiClient.get(`/${name}`);
  },
  searchEntries(phonebookQuery, searchQuery) {
    return apiClient.get(
      `/findentries?phoneBookName=${phonebookQuery}&entryName=${searchQuery}`
    );
  },
  addEntry(phonebookQuery, entryData) {
    return apiClient.put(
      `/addentry?phoneBookName=${phonebookQuery}`,
      entryData
    );
  },
  createPhonebook(phoneBookName) {
    return apiClient.post("/create", phoneBookName);
  },
};
