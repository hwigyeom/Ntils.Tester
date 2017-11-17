import { mapGetters, mapActions } from 'vuex';
import Message from './Message.vue';

export default {
  components: { Message },
  computed: mapGetters(['messages', 'lastFetchedMessageDate']),
  methods: mapActions(['fetchMessages']),
  created () {
    return this.$store.dispatch('fetchInitialMessages')
  }
}