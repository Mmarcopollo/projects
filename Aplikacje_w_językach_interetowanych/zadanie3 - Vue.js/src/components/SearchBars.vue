<template>
    <div class="container">
        <h1>Search Bars</h1>
        <div class="form-group row">
            <button v-on:click="wyczysc()" id="clearButton">Wyszyść</button>
        </div>
        <div class="form-group">
            <label for=inputTitle>Tytuł</label>
            <input v-model="searchTytul" type="text" id=inputTitle class="form-control" placeholder="Podaj tytuł lub fragment tytułu filmu"/>
        </div>
        <div class="form-group row">
          <label class="col-sm-4 col-form-label" for="inputProductionFrom">Rok produkcji od:</label>
          <div class="col-sm-8">
              <input v-model="searchDataStart" type="text" id=inputProductionFrom class="form-control" placeholder="Liczba naturalna z przedziału 1900-2019"/>
          </div>
        </div>
        <div class="form-group row">
            <label class="col-sm-4 col-form-label" for="inputProductionTo">Rok produkcji do:</label>
            <div class="col-sm-8">
                <input v-model="searchDataKoniec" type="text" id=inputProductionTo class="form-control" placeholder="Liczba naturalna z przedziału 1900-2019"/>
            </div>
        </div>
        <div class="form-group">
          <label for="inputCast">Obsada</label>
          <input type="text" id="inputCast" class="form-control" v-model="searchObsada" placeholder="Imię i nazwisko"/>
        </div>
        <div class="form-group row">
            <button v-on:click="szukaj()" id="searchButton">Szukaj</button>
        </div>
    </div>
</template>

<script>
import EventBus from '../eventBus'

export default {
    name: "SearchBars",
    data() {
        return {
            searchTytul:'',
            searchDataStart:'',
            searchDataKoniec: '',
            searchObsada:''
        }
    },
    methods: {
        szukaj() {
            let obiektZSearchBar = {
                searchTytul: this.searchTytul,
                searchDataStart: this.searchDataStart,
                searchDataKoniec: this.searchDataKoniec,
                searchObsada: this.searchObsada
            }
            EventBus.$emit('filtrowankoFilmow', obiektZSearchBar);
        },
        wyczysc() {
            this.searchTytul = '',
            this.searchDataStart = '',
            this.searchDataKoniec = '',
            this.searchObsada = ''

            let obiektZSearchBar = {
                searchTytul: this.searchTytul,
                searchDataStart: this.searchDataStart,
                searchDataKoniec: this.searchDataKoniec,
                searchObsada: this.searchObsada
            }
            EventBus.$emit('filtrowankoFilmow', obiektZSearchBar);
        }
    }
}
</script>

<style scoped>
.form-group {
    text-align: left;
}
</style>