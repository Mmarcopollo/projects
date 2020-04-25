<template>
  <div id="app">
    <SearchBars />
    <TableWithFilms v-bind:filteredFilms="filteredFilms" />
    <div class="form-group row">
      <button v-on:click="rozwin()" id="searchButton">Pokaż więcej</button>
      <button v-on:click="reset()" id="clearButton">Resetuj do 10</button>  
    </div>      
    <ListOfGenres :hundredMovies="hundredMovies" />
    <ListOfCasts :hundredMoviesActors="hundredMoviesActors" />    
  </div>
</template>

<script>
import EventBus from './eventBus'
import TableWithFilms from './components/TableWithFilms'
import SearchBars from './components/SearchBars'
import jsonMovies from './data/movies.json'
import ListOfGenres from './components/ListOfGenres'
import ListOfCasts from './components/ListOfCasts'

var counter = 10;

export default {
  name: 'app',
  components: {
    TableWithFilms,
    SearchBars,
    ListOfGenres,
    ListOfCasts
  },
  data() {
    return {
        filteredFilms: jsonMovies.slice(0,counter),
        movies: jsonMovies,
        hundredMovies: jsonMovies.slice(1000, 1100),
        hundredMoviesActors: jsonMovies.slice(1000, 1100),
        allMovies: jsonMovies.slice(0),
    }
  },
  mounted () {
        EventBus.$on('filtrowankoFilmow', (obiektZSearchBar) => {
            let cyk = this.filtrowankoFilmow(obiektZSearchBar)
            this.movies = cyk
            this.filteredFilms = this.movies.slice(0, counter)
            window.console.log(cyk)
            window.console.log(this.filteredFilms)
        });
    },
    methods: {
      filtrowankoFilmow(obiektZSearchBar) {
        window.console.log(obiektZSearchBar)
            return this.allMovies.filter((movie)=>{
                if (obiektZSearchBar.searchObsada != '') {
                    if(obiektZSearchBar.searchDataKoniec == '' || obiektZSearchBar.searchDataKoniec < 1000) {
                        return movie.title.includes(obiektZSearchBar.searchTytul) && (movie.year >= obiektZSearchBar.searchDataStart && movie.year <= new Date().getFullYear()) && movie.cast.includes(obiektZSearchBar.searchObsada);
                    }
                    else
                        return movie.title.includes(obiektZSearchBar.searchTytul) && (movie.year >= obiektZSearchBar.searchDataStart && movie.year <= obiektZSearchBar.searchDataKoniec) && movie.cast.includes(obiektZSearchBar.searchObsada);
                }
                else {
                    if(obiektZSearchBar.searchDataKoniec == '' || obiektZSearchBar.searchDataKoniec < 1000) {
                        return movie.title.includes(obiektZSearchBar.searchTytul) && (movie.year >= obiektZSearchBar.searchDataStart && movie.year <= new Date().getFullYear());
                    }
                    else {
                        return movie.title.includes(obiektZSearchBar.searchTytul) && (movie.year >= obiektZSearchBar.searchDataStart && movie.year <= obiektZSearchBar.searchDataKoniec);
                    }
                }
            })
        },
        przekaz100Filow(){
            EventBus.$emit('filmyWedlugRodzaju', this.hundredMovies);
        },
        rozwin() {
            counter += 10;
            this.filteredFilms = this.movies.slice(0, counter)
            window.console.log(this.filteredFilms)
            window.console.log(counter)
        },
        reset() {
            counter = 10;
            this.filteredFilms = this.movies.slice(0, counter)
            window.console.log(this.filteredFilms)
            window.console.log(counter)
        }
    }
}
</script>

<style>
#app {
  font-family: 'Avenir', Helvetica, Arial, sans-serif;
  -webkit-font-smoothing: antialiased;
  -moz-osx-font-smoothing: grayscale;
  text-align: center;
  color: #2c3e50;
  margin-top: 60px;
}

#searchButton {
    background-color: #4CAF50; /* Green */
    border: none;
    color: white;
    padding: 15px 32px;
    text-align: center;
    text-decoration: none;
    display: inline-block;
    font-size: 16px;
    border-radius: 12px;
    margin: auto
}

#clearButton {
    background-color: rgb(247, 161, 1); /* Green */
    border: none;
    color: white;
    padding: 15px 32px;
    text-align: center;
    text-decoration: none;
    display: inline-block;
    font-size: 16px;
    border-radius: 12px;
    margin: auto 
}
</style>
