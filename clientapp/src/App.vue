<template>
  <div class="container">
    <Header />
    <Films
      @add-film="onAddFilm"
      @update-film="onUpdateFilm"
      @delete-film="onDeleteFilm"
      :films="films"
    />
    <Modal
      :modalHeader="modalHeader"
      :modalYear="modalYear"
      :modalTitle="modalTitle"
      :modalId="modalId"
      v-show="isModalVisible"
      @close="closeModal"
      @submit="submitModal"
    />
    <Footer />
  </div>
</template>

<script>
import Modal from "./components/Modal.vue";
import Header from "./components/Header.vue";
import Footer from "./components/Footer.vue";
import Films from "./components/Films.vue";
import axios from "axios";

export default {
  name: "App",
  components: {
    Header,
    Footer,
    Films,
    Modal,
  },
  data() {
    return {
      films: [],
      isModalVisible: false,
      modalHeader: "",
      modalId: null,
      modalTitle: null,
      modalYear: null,
      isModalAdding: false,
      isModalUpdating: false,
      loading: true,
    };
  },
  methods: {
    async onDeleteFilm(id, index) {
      if (confirm("Do you want to delete " + this.films[index].title + "?")) {
        await this.deleteFilm(id);
        await this.getFilmsList();
      } else return;
    },
    onUpdateFilm(id, index) {
      this.isModalUpdating = true;
      this.isModalVisible = true;
      var exactFilm = this.films[index];
      this.modalId = id;
      this.modalTitle = exactFilm.title;
      this.modalYear = exactFilm.year;
      this.modalHeader = "Update Film";
    },
    onAddFilm() {
      this.isModalAdding = true;
      this.isModalVisible = true;
      this.modalTitle = "";
      this.modalYear = null;
      this.modalHeader = "Add new Film";
    },
    closeModal() {
      this.isModalVisible = false;
    },
    async submitModal(id, title, year) {
      if (this.isModalAdding) {
        await this.addFilm(title, year);
        await this.getFilmsList();
        this.isModalAdding = false;
      } else if (this.isModalUpdating) {
        await this.updateFilm(id, title, year);
        await this.getFilmsList();
        this.isModalUpdating = false;
      }
      this.closeModal();
    },
    async getFilmsList() {
      try {
        const response = await axios.get("/api/GetFilms");
        alert(response);
      } catch (error) {
        alert(error+"error");
      }
    },
    async addFilm(title, year) {
        await axios
          .post("/api/AddFilm/"+title+"/"+year)
          .then(function (response) {
            alert("OK"+response);
          })
          .catch(function (error) {
            alert(error);
          });
    },
    async updateFilm(id, title, year) {
        await axios
          .patch("/api/PatchFilm/"+id+"/"+title+"/"+year)
          .then(function (response) {
            alert("OK"+response);
          })
          .catch(function (error) {
            alert(error);
          });
    },
    async deleteFilm(id) {
      const path = "/api/DeleteFilm/" + id;
      await axios
        .delete(path)
        .then(function (response) {
          alert("OK"+response);
        })
        .catch(function (error) {
          alert(error);
        });
    },
  },
  mounted() {
    this.getFilmsList();
  },
  created() {
    this.films = [
      {
        id: 1,
        title: "Assasins Creed",
        year: 2019,
      },
      {
        id: 2,
        title: "People",
        year: 1997,
      },
      {
        id: 11,
        title: "Boom",
        year: 2006,
      },
      {
        id: 4,
        title: "Assasins Creed",
        year: 2019,
      },
      {
        id: 5,
        title: "People",
        year: 1997,
      },
      {
        id: 6,
        title: "Boom",
        year: 2006,
      },
      {
        id: 7,
        title: "People",
        year: 1997,
      },
      {
        id: 8,
        title: "Boom",
        year: 2006,
      },
      {
        id: 9,
        title: "Assasins Creed",
        year: 2019,
      },
    ];
  },
};
</script>

<style>
body {
  background-color: #dbdbdb;
  font-family: "Lato", sans-serif;
  margin: 0;
}

.btn {
  width: 80px;
  height: 30px;
  border: 0;
  color: #fff;
  border-radius: 5px;
}

.container {
}

.container > * {
}
</style>
