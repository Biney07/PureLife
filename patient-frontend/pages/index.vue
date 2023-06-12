<template>
  <Header />
  <HeroSectionVue />
  <SpecializimetSectionVue />
  <FeaturesSectionVue />
</template>

<script>
import FeaturesSectionVue from '~/sections/FeaturesSection.vue'
import HeroSectionVue from '~/sections/HeroSection.vue'
import SpecializimetSectionVue from '~/sections/SpecializimetSection.vue'
import Header from "../components/Header.vue"
export default {
    components: {
        Header,
        HeroSectionVue,
        SpecializimetSectionVue,
        FeaturesSectionVue
    }
}
</script>

<style>

</style>